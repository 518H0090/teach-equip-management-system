<script setup>
import { onMounted, reactive, ref } from "vue";
import axios from "axios";
import router from "@/router";
import { useStore } from "vuex";
import { useToast } from "vue-toastification";

const store = useStore();
const aside = document.querySelector("aside");

aside.style.display = "none";

const form = reactive({
  username: "",
  password: "",
});

const toast = useToast();

onMounted(async () => {
  localStorage.removeItem("access_token");
  localStorage.removeItem("refresh_token");
  await store.dispatch("setAuth", false);
  // await store.dispatch("setIsExpanded", false);
});

const isProcess = ref(true);

const handleLogin = async () => {
  const username = document.querySelector("#username");
  const password = document.querySelector("#password");

  const usernameValue = username.value.trim();
  const passwordValue = password.value.trim();

  let isProcess = true;

  if (usernameValue === "") {
    toast.error("Username can't empty")
    isProcess = false;
  } 

  if (passwordValue === "") {
    toast.error("Password can't empty")
    isProcess = false;
  } 

  if (isProcess) {
    const loginInfo = {
      username: form.username,
      password: form.password,
    };

    try {
      const response = await axios.post(
        "https://localhost:7112/api/usermanage/login",
        loginInfo
      );

      if (response.data.statusCode === 200) {
        localStorage.setItem("access_token", response.data.data.accessToken);
        localStorage.setItem("refresh_token", response.data.data.refreshToken);
        await store.dispatch("setAuth", true);
        // await store.dispatch("setIsExpanded", localStorage.getItem("is_expanded"));
        aside.style.display = "flex";
        // router.push("/").then(() => router.go());
        toast.success("Login Successful")
        router.push("/").then(() => router.go());
      }
    } catch (error) {
      toast.error("Login Failed")
      console.log("Error Fetching jobs", error);
      await store.dispatch("setAuth", false);
    }
  }
};

</script>

<template>
  <section
    class="login-section bg-gradient-to-r from-[#F28383] from-10% via-[#9D6CD2] via-30% to-[#481EDC] to-90% flex items-center justify-center h-screen w-screen"
  >
    <div
      class="max-w-[960px] bg-black-dark grid grid-cols-2 items-center gap-20 p-5 rounded-2xl move-view"
    >
      <div class="relative hidden sm:block">
        <img src="../assets/signup-background.svg" alt="" />
        <img src="../assets/teamwork.svg" alt="" class="absolute top-36" />
      </div>
      <div class="max-w-80 mx-auto grid gap-5">
        <h1 class="text-5xl font-bold text-white">Login</h1>
        <p class="text-dull-white">
          Welcome to access to my system, if you don't have any account please
          contact admin
        </p>
        <form
          action=""
          @submit.prevent="handleLogin"
          class="space-y-6 text-white"
        >
          <div class="relative input-control mb-4">
            <div
              class="move-icon absolute top-1 left-1 bg-white-medium rounded-full p-2 flex items-center justify-center text-blue-300"
            >
              <span class="material-icons">account_circle</span>
            </div>
            <input
              id="username"
              name="username"
              v-model="form.username"
              type="text"
              placeholder="Username"
              class="w-80 bg-white-light py-2 px-12 rounded-full focus:bg-black-dark focus:outline-none focus:ring-1 focus:ring-neon-blue focus:drop-shadow-lg"
            />
            <div
              class="error absolute block text-gray-700 font-bold mb-2"
            ></div>
          </div>
          <div class="relative input-control mb-4">
            <div
              class="move-icon absolute top-1 left-1 bg-white-medium rounded-full p-2 flex items-center justify-center text-blue-300"
            >
              <span class="material-icons">lock</span>
            </div>
            <input
              id="password"
              name="password"
              v-model="form.password"
              type="password"
              placeholder="Password"
              class="w-80 bg-white-light py-2 px-12 rounded-full focus:bg-black-dark focus:outline-none focus:ring-1 focus:ring-neon-blue focus:drop-shadow-lg"
            />
            <div
              class="error absolute block text-gray-700 font-bold mb-2"
            ></div>
          </div>
          <button
            class="bg-gradient-to-r from-blue-400 to-cyan-200 w-80 font-semibold rounded-full py-2"
          >
            Sign in
          </button>
        </form>
        <div
          class="text-dull-white border-t border-white-light pt-4 space-y-4 text-sm"
        >
          <!-- <p>
            Don't have an account
            <a class="text-neon-blue font-semibold cursor-pointer">Sign up</a>
          </p>
          <p>
            Forgot password?
            <a class="text-neon-blue font-semibold cursor-pointer"
              >Reset password</a
            >
          </p> -->
          <!-- <p>
            Don't have a password yet?
            <a class="text-neon-blue font-semibold cursor-pointer"
              >Set password</a
            >
          </p> -->
        </div>
      </div>
    </div>
  </section>
</template>

<style lang="scss" scoped>
aside {
  display: none;
}

.move-icon {
  margin-top: -0.2rem;
  margin-left: -0.16rem;
}

@media (max-width: 640px) {
  .move-view .max-w-80 {
    padding-left: 6rem;
    padding-right: 2rem;
  }
}

@media (max-width: 540px) {
  .move-view .max-w-80 {
    padding-left: 4rem;
    padding-right: 2rem;
  }
}

</style>
