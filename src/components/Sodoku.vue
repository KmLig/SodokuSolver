<template>
  <div class="sodoku-component">
    <h1>Start to play!</h1>
    <div class="sudoku-grid">
      <div
        v-for="(row, rowIndex) in grid"
        :key="'row-' + rowIndex"
        class="sudoku-row"
      >
        <input
          v-for="(cell, colIndex) in row"
          :key="'cell-' + rowIndex + '-' + colIndex"
          type="text"
          v-model="grid[rowIndex][colIndex]"
          @input="validateInput(rowIndex, colIndex)"
          maxlength="1"
          @paste="handlePaste"
        />
      </div>
    </div>
    <div class="button-container">
      <button
        :class="['button', 'solve-button']"
        :disabled="!isValid"
        @click="solveSudoku"
      >
        Solve
      </button>
      <button :class="['button', 'reset-button']" @click="resetGrid">
        Reset
      </button>
    </div>

    <p class="errorMessage" v-if="errorMessage">{{ errorMessage }}</p>
    <dx-toast
      :message="toastMessage"
      :visible.sync="showToast"
      :close-on-click="true"
      :display-timeout="5000"
      :width="300"
      :height="80"
      :position="{ at: 'top', my: 'top', offset: '0 50' }"
      @hidden="onToastHidden"
    />
  </div>
</template>

<script>
import { ref, computed } from "vue";
import axios from "axios";

export default {
  setup() {
    const grid = ref(Array.from({ length: 9 }, () => Array(9).fill("")));
    const errorMessage = ref("");
    const isValid = computed(() => !errorMessage.value);

    const validateInput = (row, col) => {
      const val = grid.value[row][col];
      if (val && !/^[1-9]$/.test(val)) {
        grid.value[row][col] = "";
        errorMessage.value = "Invalid input. Only numbers 1-9 allowed";
      } else if (!isSafe(grid.value, row, col, val)) {
        errorMessage.value =
          "Invalid input. No duplicates in rows, columns, or 3x3 blocks.";
      } else {
        errorMessage.value = "";
      }
    };

    const isSafe = (currentGrid, row, col, val) => {
      if (!val) return true;

      for (let i = 0; i < 9; i++) {
        if (
          (currentGrid[row][i] === val && i !== col) ||
          (currentGrid[i][col] === val && i !== row)
        ) {
          return false;
        }
      }
      const startRow = Math.floor(row / 3) * 3;
      const startCol = Math.floor(col / 3) * 3;
      for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 3; j++) {
          if (
            currentGrid[startRow + i][startCol + j] === val &&
            (startRow + i !== row || startCol + j !== col)
          ) {
            return false;
          }
        }
      }
      return true;
    };

    const solveSudoku = async () => {
      const gridNumbers = grid.value.map((row) =>
        row.map((cell) => (cell === "" ? 0 : parseInt(cell)))
      );

      if (solve(gridNumbers)) {
        grid.value = gridNumbers.map((row) =>
          row.map((num) => (num === 0 ? "" : num.toString()))
        );
        errorMessage.value = "";

        const payload = {
          solution: gridNumbers.flat().join(""),
        };

        try {
          const response = await axios.post(
            "https://localhost:7103/api/Sudoku/solve",
            payload
          );
          console.log(response.data.message);
        } catch (error) {
          console.error("Error saving solution:", error);
          errorMessage.value = "Failed to save the solution.";
        }
      } else {
        errorMessage.value = "The Sudoku puzzle is not solvable.";
      }
    };

    const findEmptyCell = (board) => {
      for (let row = 0; row < 9; row++) {
        for (let col = 0; col < 9; col++) {
          if (board[row][col] === 0) {
            return { row, col };
          }
        }
      }
      return null;
    };

    const solve = (board) => {
      const emptyCell = findEmptyCell(board);

      if (!emptyCell) {
        return true;
      }

      const { row, col } = emptyCell;

      for (let num = 1; num <= 9; num++) {
        if (isSafe(board, row, col, num)) {
          board[row][col] = num;

          if (solve(board)) {
            return true;
          }

          board[row][col] = 0;
        }
      }
      return false;
    };

    const handlePaste = (event) => {
      event.preventDefault();
      const pastedData = event.clipboardData.getData("text/plain");
      const values = pastedData.split(/\s+/);

      for (let i = 0; i < 9; i++) {
        for (let j = 0; j < 9; j++) {
          const value = values[i * 9 + j];
          if (value && /^[1-9]$/.test(value)) {
            grid.value[i][j] = value;
          } else {
            grid.value[i][j] = "";
          }
        }
      }
    };

    const resetGrid = () => {
      grid.value = Array.from({ length: 9 }, () => Array(9).fill(""));
      errorMessage.value = "";
    };

    return {
      grid,
      errorMessage,
      isValid,
      validateInput,
      solveSudoku,
      handlePaste,
      resetGrid,
    };
  },
};
</script>

<style scoped lang="scss">
.sodoku-component {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  text-align: center;

  .sudoku-grid {
    display: grid;
    grid-template-columns: repeat(9, 40px);

    .sudoku-row {
      display: contents;
    }

    input {
      width: 40px;
      height: 40px;
      text-align: center;
      font-size: 1.2em;

      &:focus {
        background-color: #bedaec;
      }
    }
  }

  .button-container {
    display: flex;
    justify-content: center;
    gap: 10px;
    margin-top: 20px;

    .button {
      padding: 10px 20px;
      margin: 5px;
      font-size: 1em;
      color: #4caf50;
      border: none;
      border-radius: 5px;
      cursor: pointer;
      transition: background-color 0.3s ease;

      &:disabled {
        background-color: #cccccc !important;
        cursor: not-allowed;
      }

      &:active {
        transform: scale(0.98);
      }

      &.solve-button {
        border: 1px solid #4caf50;
        color: #4caf50;

        &:hover:not(:disabled) {
          border: 1px solid #45a049;
        }
      }

      &.reset-button {
        // background-color: #f44336;
        border: 1px solid #f44336;
        color: #f44336;

        &:hover:not(:disabled) {
          border: 1px solid #e53935;
        }
      }
    }
  }

  .errorMessage {
    color: red;
  }
}
</style>
