<script setup>
import Greeting from "./components/Greeting.vue";
import Sodoku from "./components/Sodoku.vue";
import SavedSudoku from "./components/SavedSudoku.vue";
</script>

<template>
  <div class="left-part">
    <div class="left-part--heading">
      <img
        alt="Vue logo"
        class="logo"
        src="./assets/logo.svg"
        width="50"
        height="50"
      />

      <div class="wrapper">
        <Greeting msg="Sodoku solver!" />
      </div>
    </div>
    <Sodoku />
  </div>

  <div class="right-part">
    <SavedSudoku />
  </div>
</template>

<style scoped>
.left-part {
  line-height: 1.5;
  padding-top: 25px;
}

.right-part {
  padding-top: 25px;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  .left-part--heading {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  .left-part--heading .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
