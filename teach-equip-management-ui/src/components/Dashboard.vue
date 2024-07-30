<script setup>
import { onBeforeMount, onMounted, onUnmounted, ref, defineProps, computed } from "vue";
import axios from "axios";
import Chart from "chart.js/auto";

const props = defineProps({
  users: Array,
  tools: Array,
});

const histories = ref({});

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

let myChart = null;
let myBarChart = null;

onMounted(async () => {
  await allHistories();

  const dataPie = {
    labels: ["Borrow", "Return", "Buy", "Sell"],
    datasets: [
      {
        label: "Inventory History",
        data: [
          histories.value.borrow,
          histories.value.return,
          histories.value.buy,
          histories.value.sell,
        ],
        backgroundColor: [
          "rgb(255, 99, 132)",
          "rgb(54, 162, 235)",
          "rgb(255, 205, 86)",
          "rgb(126, 205, 86)",
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

const allHistories = async () => {
  try {
    const response = await axios.get(
      `https://localhost:7112/api/inventorymanage/all-inventory-histories`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const dataJson = response.data.data;

    const mappedData = {
      all: dataJson.length,
      borrow: dataJson.filter((history) => history.actionType === "Borrow").length,
      return: dataJson.filter((history) => history.actionType === "Return").length,
      buy: dataJson.filter((history) => history.actionType === "Buy").length,
      sell: dataJson.filter((history) => history.actionType === "Sell").length,
      latest5Items: dataJson
        .sort((a, b) => new Date(b.actionDate) - new Date(a.actionDate))
        .slice(0, 5)
        .map( (item, index) => ({
          index: (index + 1),
          actionType: item.actionType,
          actionDate: formatDate(item.actionDate),
        })),
    };

    histories.value =  mappedData;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    // if (error.response.status === 401) {
    //   console.log("Error Fetching jobs", error);
    // }
  }
};

function formatDate(timestamp) {
  const date = new Date(timestamp);

  const day = String(date.getDate()).padStart(2, "0");
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const year = date.getFullYear();
  const hours = String(date.getHours() % 12 || 12).padStart(2, "0");
  const minutes = String(date.getMinutes()).padStart(2, "0");
  const seconds = String(date.getSeconds()).padStart(2, "0");

  return `${day}/${month}/${year} ${hours}:${minutes}:${seconds}`;
}

const inventoryById = async (inventoryId) => {
  try {
    const inventory = await axios.get(
      `https://localhost:7112/api/inventorymanage/inventory/find/${inventoryId}`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const { data } = inventory.data;

    const { toolId, ...rest } = data;

    const tool = await toolById(toolId);

    let dataResponse = {
      ...rest,
      tool,
    };

    return dataResponse;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      console.log("Error Fetching jobs", error);
    }
  }
};

const toolById = async (toolId) => {
  try {
    const tool = await axios.get(
      `https://localhost:7112/api/toolmanage/tool/find/${toolId}`,
      {
        headers: {
          Authorization: "Bearer " + localStorage.getItem("access_token"),
        },
      }
    );

    const { data } = tool.data;

    return data.toolName;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    if (error.response.status === 401) {
      console.log("Error Fetching jobs", error);
    }
  }
};
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

                <p class="total-show">
                  {{ props.users > 0 ? props.users : 0 }}
                </p>
              </div>
            </div>
            <div
              class="max-w-sm rounded overflow-hidden shadow-lg bg-gradient-to-r from-red-50 to-blue-100 ml-4"
            >
              <div class="px-6 py-4">
                <div class="font-bold text-xl mb-2 uppercase">Tool</div>
                <p
                  class="total-show"
                >
                  {{ props.tools > 0 ? props.tools : 0 }}
                </p>
             
              </div>
            </div>
            <div
              class="max-w-sm rounded overflow-hidden shadow-lg bg-gradient-to-r from-red-50 to-blue-100 ml-4"
            >
              <div class="px-6 py-4">
                <div class="font-bold text-xl mb-2 uppercase">History</div>
                <p class="total-show">
                  {{ histories.all > 0 ? histories.all : 0}}
                </p>
              </div>
            </div>
          </div>
          <!-- Card -->
          <div class="dashboard">
            <div class="info pie">
              <h1 class="border rounded w-full py-2 px-3 text-lg font-bold">History</h1>
              <canvas id="myChart"></canvas>
            </div>
            <table class="text-left text-sm font-light text-surface dark:text-white">
              <thead class="border-b border-neutral-200 font-medium dark:border-white/10">
                <tr>
                  <th class="px-4 py-3 uppercase">Index</th>
                  <th class="px-4 py-3 uppercase">ACTIONTYPE</th>
                  <th class="px-4 py-3 uppercase">ACTIONDATE</th>
                </tr>
              </thead>
              <tbody>
                <tr
                  class="border-b"
                  v-for="item of histories.latest5Items"
                  :key="item.id"
                >
                  <td
                    class="px-4 py-3 font-medium text-gray-900"
                    v-for="value of item"
                    :key="value"
                  >
                    {{ value }}
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <div class="dashboard">
            <div class="info bar">
              <h1>Chart</h1>
              <canvas id="myBarChart"></canvas>
            </div>
            <table class="text-left text-sm font-light text-surface dark:text-white">
              <thead class="border-b border-neutral-200 font-medium dark:border-white/10">
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

.total-show {
  font-size: 4rem;
  padding: auto;
}
</style>
