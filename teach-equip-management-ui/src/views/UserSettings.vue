<script setup>
import Navbar from "@/components/Navbar.vue";
import MainCard from "@/components/MainCard.vue";
import { ref, onMounted, reactive } from "vue";
import { jwtDecode } from "jwt-decode";
import axios from "axios";
import { useToast } from "vue-toastification";
import router from "@/router";
const profileSrc = ref("src/assets/avatarcapybara.jpg");

const onFileChange = (event) => {
  const file = event.target.files[0];
  if (file) {
    profileSrc.value = URL.createObjectURL(file);
    form.avatar = file;
  }
};

const toast = useToast();

const form = reactive({
  accountId: "",
  fullname: "",
  address: "",
  phone: "",
  avatar: "",
  spoFileId: "",
});

onMounted(async () => {
  await decodeJwtToken(token.value, user);
  await userDetailById(user.value.id);
});

const token = ref(localStorage.getItem("access_token") || "");
const user = ref({});

const decodeJwtToken = async (token, userRef) => {
  try {
    if (token) {
      const decoded = jwtDecode(token);

      userRef.value = {
        exp: decoded.exp,
        id:
          decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
        name: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
        role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
      };
    }
  } catch (error) {
    console.error("Invalid token:", error);
  }
};

const userDetailById = async (accountId) => {
  try {
    const response = await axios.get(
      `https://localhost:7112/api/usermanage/user-detail/find/${accountId}`
    );

    const datajson = response.data.data;

    form.accountId = datajson.accountId;
    form.fullname = datajson.fullName;
    form.phone = datajson.phone;
    form.avatar = datajson.avatar;
    form.spoFileId = datajson.spoFileId;
    form.address = datajson.address;

    if (form.avatar !== "") {
      profileSrc.value = form.avatar;
      localStorage.setItem("profileSrc", form.avatar)
    }

  } catch (error) {
    console.log("Error Fetching SupplierInfo", error);
  }
};

const setError = (element, message) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");
  errorDisplay.innerText = message;
  inputControl.classList.add("error");
  inputControl.classList.remove("success");
};

const setSuccess = (element) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");
  errorDisplay.innerText = "";
  inputControl.classList.add("success");
  inputControl.classList.remove("error");
};

const validateInputs = async () => {
  const fullname = document.querySelector("#fullname");
  const address = document.querySelector("#address");
  const phone = document.querySelector("#phone");

  const fullnameValue = fullname.value.trim();
  const addressValue = address.value.trim();
  const phoneValue = phone.value.trim();

  const regexPhone = /^0?\d{8,14}$/;
  let isProcess = true;

  if (fullnameValue === "") {
    setError(fullname, "This is required");
    isProcess = false;
  } else if (fullnameValue.length > 30) {
    setError(fullname, "Fullname must less than 30 character");
    isProcess = false;
  } else {
    setSuccess(fullname);
  }

  if (addressValue === "") {
    setError(address, "This is required");
    isProcess = false;
  } else if (addressValue.length > 50) {
    setError(address, "Fullname must less than 50 character");
    isProcess = false;
  } else {
    setSuccess(address);
  }

  if (phoneValue === "") {
    setError(phone, "This is required");
    isProcess = false;
  } else if (!regexPhone.test(phoneValue)) {
    setError(phone, "Not follow format of phone");
    isProcess = false;
  } else if (regexPhone.test(phoneValue)) {
    setSuccess(phone);
  }

  if (isProcess) {
    const updateUserDetail = {
      accountId: form.accountId,
      fullName: form.fullname,
      address: form.address,
      phone: form.phone,
      fileUpload: typeof form.avatar === "string" ? null : form.avatar,
    };

    const formData = new FormData();
    formData.append("AccountId", updateUserDetail.accountId);
    formData.append("FullName", updateUserDetail.fullName);
    formData.append("Address", updateUserDetail.address);
    formData.append("Phone", updateUserDetail.phone);
    formData.append("FileUpload", updateUserDetail.fileUpload);

    const response = fetch("https://localhost:7112/api/usermanage/update-user-detail", {
      method: "PUT",
      body: formData,
    })
      .then((response) => {
        return response.json();
      })
      .then(async (data) => {
        if (data.statusCode !== 202) {
          console.error("Error fetching data:", data.statusCode, data.message);
        }

        if (data.statusCode === 202) {
          toast.success("success edit user detail");
          localStorage.setItem("profileSrc", form.avatar)
          router.go()
        }
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
        toast.error("Error edit user detail");
      });
  } else {
    toast.error("Something error");
  }
};
</script>

<template>
  <div class="content">
    <Navbar hideProfile="true" />
    <MainCard>
      <section class="bg-green-50">
        <div class="container m-auto">
          <div class="bg-white shadow-md rounded-md border m-4 px-6 md:m-0">
            <form @submit.prevent="validateInputs">
              <h2 class="text-3xl text-center font-semibold mb-6">User Profile</h2>
  
              <div class="hero">
                <div class="card">
                  <img :src="`${profileSrc}`" alt="capybara" id="profile-pic" />
                  <label for="input-file">Update Image</label>
                  <input
                    @change="onFileChange"
                    type="file"
                    accept="image/jpeg, image/png, image/jpg"
                    id="input-file"
                  />
                </div>
              </div>
  
              <h3 class="text-2xl mb-5">User Info</h3>
  
              <div class="input-control mb-4">
                <label class="block text-gray-700 font-bold mb-2">Fullname</label>
                <input
                  v-model="form.fullname"
                  type="text"
                  id="fullname"
                  name="fullname"
                  class="border rounded w-full py-2 px-3 mb-2"
                  placeholder="eg. Nguyễn Văn A"
                />
  
                <div class="error block text-gray-700 font-bold mb-2"></div>
              </div>
  
              <div class="input-control mb-4">
                <label class="block text-gray-700 font-bold mb-2"> Address </label>
                <input
                  v-model="form.address"
                  type="text"
                  id="address"
                  name="address"
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
                  v-model="form.phone"
                  type="text"
                  id="phone"
                  name="phone"
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
  </div>
 
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

.input-control.success {
  border: 2px solid #09c372;
  padding: 0.4rem;
  border-radius: 10px;
  display: block;

  input:focus,
  textarea:focus {
    outline: 0;
  }
}

.input-control.error {
  border: 2px solid #ff3860;
  padding: 0.4rem;
  border-radius: 10px;
  display: block;
  color: inherit;

  input:focus,
  textarea:focus {
    outline: 0;
  }
}

.error {
  color: #ff3860;
  padding: 1rem 0.4rem;
}
</style>
