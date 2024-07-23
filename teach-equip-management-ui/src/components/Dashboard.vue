<script setup>
import { onMounted } from "vue";

import Chart from "chart.js/auto";

const labels = ["January", "February", "March"];

const data = {
  labels: labels,
  datasets: [
    {
      label: "First",
      backgroundColor: "rgb(255, 99, 132)",
      borderColor: "rgb(255, 99, 132)",
      data: [0, 10, 5, 2, 20, 30, 45],
    },
    {
      label: "Second",
      backgroundColor: "rgb(54, 162, 235)",
      borderColor: "rgb(54, 162, 235)",
      data: [0, 109, 50, 20, 200, 30, 450],
    },
    {
      label: "Third",
      backgroundColor: "rgb(255, 205, 86)",
      borderColor: "rgb(255, 205, 86)",
      data: [10, 5, 220, 40, 230, 65, 110],
    },
  ],
};

const config = {
  type: "bar",
  data: data,
  options: {},
};

const dataPie = {
  labels: ["Red", "Blue", "Yellow"],
  datasets: [
    {
      label: "My First Dataset",
      data: [300, 50, 100],
      backgroundColor: [
        "rgb(255, 99, 132)",
        "rgb(54, 162, 235)",
        "rgb(255, 205, 86)",
      ],
      hoverOffset: 4,
    },
  ],
};

const configPie = {
  type: "doughnut",
  data: dataPie,
  options: {},
};

onMounted(() => {
  const myChart = new Chart(document.getElementById("myChart"), config);
  const hehe = new Chart(document.getElementById("hehe"), configPie);
});
</script>

<template>
  <div class="dashboard">
    <div class="info bar">
      <h1>Chart</h1>
      <canvas id="myChart"></canvas>
    </div>
    <div class="info">
      <h1>Chart</h1>
      <canvas id="hehe"></canvas>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.dashboard {
  display: flex;
  justify-content: space-evenly;
  align-items: center;
}

.info {
  padding-left: 6rem;
  width: 80%;
  height: 80%;
}

@media (min-width: 1280px) {
  .info {
    width: 40%;
    height: 40%;
  }
}

@media (max-width: 800px) {
  .dashboard {
    flex-wrap: wrap;

    .info {
      padding: 1rem;
      margin-top: 2rem;
      width: 100%;
      height: 100%;
    }
  }
}
</style>
