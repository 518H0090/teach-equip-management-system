<script setup>
import { onBeforeMount, onMounted, onUnmounted, ref, defineProps, computed } from "vue";
import axios from "axios";
import Chart from "chart.js/auto";

const props = defineProps({
  users: Array,
  tools: Array,
});

const histories = ref({});

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
          histories.value.borrow.length,
          histories.value.return.length,
          histories.value.buy.length,
          histories.value.sell.length,
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

  const chartData = transformDataForChart(histories.value.groupedData);

  const data = {
    labels: chartData.labels,
    datasets: chartData.datasets,
  };

  const config = {
    type: "bar",
    data: data,
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

    const groupedData = countByDateAndType(response.data.data);

    const mappedData = {
      all: dataJson.length,
      borrow: dataJson.filter((history) => history.actionType === "Borrow"),
      return: dataJson.filter((history) => history.actionType === "Return"),
      buy: dataJson.filter((history) => history.actionType === "Buy"),
      sell: dataJson.filter((history) => history.actionType === "Sell"),
      latest5Items: dataJson
        .sort((a, b) => new Date(b.actionDate) - new Date(a.actionDate))
        .slice(0, 5)
        .map((item, index) => ({
          index: index + 1,
          actionType: item.actionType,
          actionDate: formatDate(item.actionDate),
        })),
      groupedData: groupedData,
    };

    histories.value = mappedData;
  } catch (error) {
    console.log("Error Fetching jobs", error);
    // if (error.response.status === 401) {
    //   console.log("Error Fetching jobs", error);
    // }
  }
};
const formatDateBasic = (dateStr) => dateStr.split("T")[0];

const countByDateAndType = (data) => {
  const result = {};

  const actionTypes = ["Borrow", "Return", "Buy", "Sell"];

  actionTypes.forEach((actionType) => {
    result[actionType] = data
      .filter((history) => history.actionType === actionType)
      .reduce((acc, item) => {
        const dateOnly = formatDateBasic(item.actionDate);
        if (!acc[dateOnly]) {
          acc[dateOnly] = 0;
        }
        acc[dateOnly] += 1;
        return acc;
      }, {});
  });

  return result;
};

function transformDataForChart(groupedData) {
  const dates = Array.from(
    new Set(Object.values(groupedData).flatMap((data) => Object.keys(data)))
  ).sort();

  const labels = dates.map((date) => {
    const [year, month, day] = date.split("-");
    return (
      new Date(year, month - 1, day).toLocaleString("default", {
        day: "2-digit",
        month: "long",
      }) +
      " " +
      year
    );
  });

  const datasets = Object.keys(groupedData).map((actionType) => ({
    label: actionType,
    backgroundColor: getRandomColor(),
    borderColor: getRandomColor(),
    data: dates.map((date) => groupedData[actionType][date] || 0),
  }));

  function getRandomColor() {
    const letters = "0123456789ABCDEF";
    let color = "#";
    for (let i = 0; i < 6; i++) {
      color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
  }

  return {
    labels: labels,
    datasets: datasets,
  };
}

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
                <p class="total-show">
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
                  {{ histories.all > 0 ? histories.all : 0 }}
                </p>
              </div>
            </div>
          </div>
          <!-- Card -->
          <div class="dashboard">
            <div class="info pie">
              <h3 class="border rounded w-full py-2 px-3 text-lg font-bold">History</h3>
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
          <div class="info bar">
            <h3 class="border rounded w-full py-2 px-3 text-lg font-bold">
              History Follow Day
            </h3>
            <canvas id="myBarChart"></canvas>
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
  flex: 1;

  h3 {
    margin-left: 1rem;
  }

  &.bar {
    width: 80%;
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
