<script setup>
import { onMounted, onUnmounted } from "vue";

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

let myChart = null;
let myBarChart = null;

onMounted(() => {
  myChart = new Chart(document.getElementById("myChart"), configPie);
  myBarChart = new Chart(document.getElementById("myBarChart"), config);
});

onUnmounted(() => {
  if (myChart) {
    myChart.destroy();
  }
  if (myBarChart) {
    myBarChart.destroy();
  }
});
</script>

<template>
  <div class="flex flex-col bg-white relative border rounded-lg">
    <div class="overflow-x-auto sm:-mx-6 lg:-mx-8">
      <div class="inline-block min-w-full py-2 sm:px-6 lg:px-8">
        <div class="overflow-hidden">
          <!-- Card -->
          <div class="grid grid-cols-3 flex">
            <div
              class="max-w-sm rounded overflow-hidden shadow-lg bg-gradient-to-r from-red-50 to-blue-100 ml-4"
            >
              <div class="px-6 py-4">
                <div class="font-bold text-xl mb-2 uppercase">Account</div>

                <p
                  class="text-gray-700 text-base border rounded w-full py-2 px-3 text-xl font-bold"
                >
                  <span
                    class="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
                    >Admin</span
                  >
                  : 10
                </p>
                <p
                  class="text-gray-700 text-base border rounded w-full py-2 px-3 text-xl font-bold"
                >
                  <span
                    class="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
                    >Manager</span
                  >
                  : 10
                </p>
                <p
                  class="text-gray-700 text-base border rounded w-full py-2 px-3 text-xl font-bold"
                >
                  <span
                    class="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
                    >User</span
                  >
                  : 10
                </p>
              </div>
            </div>
            <div
              class="max-w-sm rounded overflow-hidden shadow-lg bg-gradient-to-r from-red-50 to-blue-100 ml-4"
            >
              <div class="px-6 py-4">
                <div class="font-bold text-xl mb-2 uppercase">Tool</div>
                <p
                  class="text-gray-700 text-base border rounded w-full py-2 px-3 text-xl font-bold"
                >
                  <span
                    class="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
                    >Total</span
                  >
                  : 10
                </p>
                <p
                  class="text-gray-700 text-base border rounded w-full py-2 px-3 text-xl font-bold"
                >
                  <span
                    class="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
                    >Category</span
                  >
                  : 10
                </p>
                <p
                  class="text-gray-700 text-base border rounded w-full py-2 px-3 text-xl font-bold"
                >
                  <span
                    class="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
                    >Supplier</span
                  >
                  : 10
                </p>
                <p
                  class="text-gray-700 text-base border rounded w-full py-2 px-3 text-xl font-bold"
                >
                  <span
                    class="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
                    >Request</span
                  >
                  : 10
                </p>
              </div>
            </div>
            <div
              class="max-w-sm rounded overflow-hidden shadow-lg bg-gradient-to-r from-red-50 to-blue-100 ml-4"
            >
              <div class="px-6 py-4">
                <div class="font-bold text-xl mb-2 uppercase">History</div>
                <p
                  class="text-gray-700 text-base border rounded w-full py-2 px-3 text-xl font-bold"
                >
                  <span
                    class="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
                    >Borrow</span
                  >
                  : 10
                </p>
                <p
                  class="text-gray-700 text-base border rounded w-full py-2 px-3 text-xl font-bold"
                >
                  <span
                    class="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
                    >Return</span
                  >
                  : 10
                </p>
                <p
                  class="text-gray-700 text-base border rounded w-full py-2 px-3 text-xl font-bold"
                >
                  <span
                    class="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
                    >Buy</span
                  >
                  : 10
                </p>

                <p
                  class="text-gray-700 text-base border rounded w-full py-2 px-3 text-xl font-bold"
                >
                  <span
                    class="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
                    >Sell</span
                  >
                  : 10
                </p>
              </div>
            </div>
          </div>
          <!-- Card -->
          <div class="dashboard">
            <div class="info pie">
              <h1>Chart</h1>
              <canvas id="myChart"></canvas>
            </div>
            <table
              class="text-left text-sm font-light text-surface dark:text-white"
            >
              <thead
                class="border-b border-neutral-200 font-medium dark:border-white/10"
              >
                <tr>
                  <th class="px-4 py-3 uppercase">hehehehe</th>
                  <th class="px-4 py-3 uppercase">hehehehe</th>
                  <th class="px-4 py-3 uppercase">hehehehe</th>
                  <th class="px-4 py-3 uppercase">hehehehe</th>
                </tr>
              </thead>
              <tbody>
                <tr class="border-b">
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                </tr>
                <tr class="border-b">
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                </tr>
                <tr class="border-b">
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                </tr>
                <tr class="border-b">
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                </tr>
              </tbody>
            </table>
          </div>
          <div class="dashboard">
            <div class="info bar">
              <h1>Chart</h1>
              <canvas id="myBarChart"></canvas>
            </div>
            <table
              class="text-left text-sm font-light text-surface dark:text-white"
            >
              <thead
                class="border-b border-neutral-200 font-medium dark:border-white/10"
              >
                <tr>
                  <th class="px-4 py-3 uppercase">hehehehe</th>
                  <th class="px-4 py-3 uppercase">hehehehe</th>
                  <th class="px-4 py-3 uppercase">hehehehe</th>
                  <th class="px-4 py-3 uppercase">hehehehe</th>
                </tr>
              </thead>
              <tbody>
                <tr class="border-b">
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                </tr>
                <tr class="border-b">
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                </tr>
                <tr class="border-b">
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                </tr>
                <tr class="border-b">
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                  <td class="px-4 py-3 font-medium text-gray-900">Okay</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.dashboard {
  display: flex;
  align-items: center;
  justify-content: space-around;
}

.info {
  h1 {
    margin-left: 1rem;
  }

  &.bar {
    flex: 2;
  }

  &.pie {
    flex: 1;
  }
}

table {
  flex: 4;
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
