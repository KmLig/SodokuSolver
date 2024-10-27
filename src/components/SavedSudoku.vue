<template>
  <div>
    <h1>Saved solutions</h1>
    <button @click="fetchSudokuData" :disabled="isLoading">
      <span v-if="isLoading">Loading...</span>
      <span v-else>Refresh</span>
    </button>
    <dx-data-grid
      :data-source="sudokuData"
      :show-borders="true"
      :columns="columns"
      @initialized="onInitialized"
    >
      <template v-slot:headerCellTemplate="{ column }">
        <div class="header-cell">
          {{ column.dataField }}
        </div>
      </template>
    </dx-data-grid>
  </div>
</template>

<script>
import { ref, onMounted } from "vue";
import { DxDataGrid } from "devextreme-vue/data-grid";
import axios from "axios";

export default {
  components: {
    DxDataGrid,
  },
  setup() {
    const sudokuData = ref([]);
    const columns = ref([
      { dataField: "id", caption: "ID" },
      { dataField: "solution", caption: "Sudoku Solution" },
      { dataField: "solvedAt", caption: "Solve Time" },
    ]);
    const isLoading = ref(false);

    const fetchSudokuData = async () => {
      isLoading.value = true;
      try {
        const response = await axios.get("https://localhost:7103/api/Sudoku");
        sudokuData.value = response.data;
      } catch (error) {
        console.error("Error fetching Sudoku data:", error);
      } finally {
        setTimeout(() => {
          isLoading.value = false;
        }, 300);
      }
    };

    const onInitialized = () => {
      fetchSudokuData();
    };

    onMounted(() => {
      fetchSudokuData();
    });

    return {
      sudokuData,
      columns,
      fetchSudokuData,
      onInitialized,
      isLoading,
    };
  },
};
</script>

<style>
.header-cell {
  font-weight: bold;
  background-color: #f0f0f0;
}

button {
  padding: 10px 25px;
  margin: 10px;
  font-size: 1rem;
  color: #1b18de;
  background-color: #ffffff;
  border: 1px solid #1b18de;
  border-radius: 8px;
  cursor: pointer;
  transition: all 0.3s ease;
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
}

button:hover {
  border: 1px solid #1310b3;
  box-shadow: 0px 6px 10px rgba(0, 0, 0, 0.15);
}

button:active {
  border: 1px solid #0f0c93;
  transform: translateY(1px);
}

button:disabled {
  background-color: #b7b1b1;
  cursor: not-allowed;
}
</style>
