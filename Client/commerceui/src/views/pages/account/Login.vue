<template>
  <div class="bg-light min-vh-100 d-flex flex-row align-items-center">
    <CContainer>
      <CRow class="justify-content-center">
        <CCol :md="8">
          <CCardGroup>
            <CCard class="p-4">
              <CCardBody>
                <CForm>
                  <h1>Login</h1>
                  <p class="text-medium-emphasis">Sign In to your account</p>
                  <CInputGroup class="mb-3">
                    <CInputGroupText>
                      <CIcon icon="cil-user" />
                    </CInputGroupText>
                    <CFormInput placeholder="Username" autocomplete="username" v-model="username" />
                  </CInputGroup>
                  <CInputGroup class="mb-4">
                    <CInputGroupText>
                      <CIcon icon="cil-lock-locked" />
                    </CInputGroupText>
                    <CFormInput type="password" placeholder="Password" autocomplete="current-password"
                      v-model="password" />
                  </CInputGroup>
                  <CRow>
                    <CCol :xs="6">
                      <CButton color="primary" class="px-4" @click="login()"> Login </CButton>
                    </CCol>
                    <CCol :xs="6" class="text-right">
                      <CButton color="link" class="px-0">
                        Forgot password?
                      </CButton>
                    </CCol>
                  </CRow>
                </CForm>
              </CCardBody>
            </CCard>
            <CCard class="text-white bg-primary py-5" style="width: 44%">
              <CCardBody class="text-center">
                <div>
                  <h2>Sign up</h2>
                  <p>
                    Don't have an account. Signup for a free account!
                  </p>
                  <CButton color="light" variant="outline" class="mt-3" @click="goToRegister()">
                    Register Now!
                  </CButton>
                </div>
              </CCardBody>
            </CCard>
          </CCardGroup>
        </CCol>
        <div class="al">
          <ul class="text-danger">
            <li v-for="e in error" :key="e">{{ e }}</li>
          </ul>
        </div>
      </CRow>
    </CContainer>
  </div>
</template>

<script>
import { ref, computed } from 'vue'
import { useStore } from 'vuex'
import signupvalidations from '../../../services/signupvalidations';
import { useRouter } from 'vue-router';
//import api from '../../../axiosConfig';


export default {
  name: 'Login',
  setup() {
    const store = useStore()
    const email = ref('');
    const username = ref('');
    const password = ref('');
    const error = ref([]);
    const data = ref([]);
    const router = useRouter();

    // const fetchData = async () => {
    //   try {
    //     const response = await api.post('login/', { username: username.value, password: password.value });
    //     data.value = response.data;
    //     console.log(response.data);
    //   } catch (error) {
    //     console.error(error);
    //   }
    // };

    const goToRegister = () => {
      router.push({ name: 'Register' });
    }

    const login = () => {
      error.value = new signupvalidations('user@gmail.com', password, password, "0743793901").checkValidations();
      console.log(error)
      if (error.value.length) {
        return false;
      }
      if (username.value !== null && password.value !== null) {
        console.log(store)
        //fetchData();
        store.dispatch('auth/[actions] login', { username: username.value, password: password.value });
      }
    }
    return {
      username,
      password,
      error,
      email,
      data,
      login,
      //fetchData,
      goToRegister,
      storename: computed(() => store.state.auth.name),
    }
  }
}
</script>
