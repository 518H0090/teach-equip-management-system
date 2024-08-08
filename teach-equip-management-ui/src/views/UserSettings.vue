<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import { ref } from 'vue';

const profileSrc = ref('src/assets/avatarcapybara.jpg');

const onFileChange = (event) => {
  const file = event.target.files[0];
  if (file) {
    profileSrc.value = URL.createObjectURL(file);
  }
};

</script>

<template>
     <div class="content">
    <Navbar />
  </div>
  <MainCard>
    <section class="bg-green-50">
      <div class="container m-auto">
        <div class="bg-white shadow-md rounded-md border m-4 px-6 md:m-0">
          <form>
            <h2 class="text-3xl text-center font-semibold mb-6">User Profile</h2>
      
            <div class="hero">
              <div class="card">
                <img :src="`${profileSrc}`" alt="capybara" id="profile-pic">
                <label for="input-file">Update Image</label>
                <input @change="onFileChange" type="file" accept="image/jpeg, image/png, image/jpg" id="input-file"/>
              </div>
            </div>

            <h3 class="text-2xl mb-5">User Info</h3>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2"
                >Fullname</label
              >
              <input
                type="text"
                id="name"
                name="name"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg. Nguyễn Văn A"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label class="block text-gray-700 font-bold mb-2">
                Address
              </label>
              <input
                type="text"
                id="location"
                name="location"
                class="border rounded w-full py-2 px-3 mb-2"
                placeholder="eg Nguyễn Văn Trỗi"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div class="input-control mb-4">
              <label for="company" class="block text-gray-700 font-bold mb-2"
                >Phone</label
              >
              <input
                type="text"
                id="company"
                name="company"
                class="border rounded w-full py-2 px-3"
                 placeholder="eg: 0283687778"
              />

              <div class="error block text-gray-700 font-bold mb-2"></div>
            </div>

            <div>
              <button
                class="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                type="submit"
              >
                Update Profile
              </button>
            </div>
          </form>
        </div>
      </div>
    </section>
  </MainCard>
</template>

<style lang="scss" scoped>
.container {
  max-width: 980px;

  form {
    padding: 2rem 2rem;
    button {
      padding: 1rem;
    }
  }
}

.hero {
  width: 100%;
  height: 18rem;
  display: flex;
  align-items: center;
  justify-content: center;
}

.card {
  width: 480px;
  background: #fff;
  border-radius: 15px;
  text-align: center;
  color: #333;
  display: flex;
  flex-direction: column;
  align-items: center;
  box-shadow: 0.1rem 0.2rem 0.6rem var(--dark);

  img {
    width: 180x;
    height: 180px;
    margin-top: 40px;
    margin-bottom: 30px;
    justify-content: center;
  }
  

  label {
    display: block;
    width: 200px;
    background: #e3362c;
    color: #fff;
    padding: 12px;
    margin: 10px auto;
    border-radius: 5px;
    cursor: pointer;
  }

  input {
    display: none;
  }
}
</style>
