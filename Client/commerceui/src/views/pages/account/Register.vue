<template>
  <div class="bg-light min-vh-100 d-flex flex-row align-items-center">
    <CContainer>
      <CRow class="justify-content-center">
        <CCol :md="9" :lg="7" :xl="6">
          <CCard class="mx-4">
            <CCardBody class="p-4">
              <CForm @submit.prevent="register()">
                <h1>Register</h1>
                <p class="text-medium-emphasis">Create your account</p>
                <div class="row">
                  <div class="col-sm-6">
                    <CInputGroup class="mb-3">
                      <CInputGroupText>
                        <CIcon icon="cil-user" />
                      </CInputGroupText>
                      <CFormInput placeholder="Username" autocomplete="username" v-model.trim="username" />
                    </CInputGroup>
                    <CInputGroup class="mb-3">
                      <CInputGroupText>@</CInputGroupText>
                      <CFormInput placeholder="Email" autocomplete="email" v-model.trim="email" />
                    </CInputGroup>
                    <CInputGroup class="mb-3">
                      <CInputGroupText>
                        <CIcon icon="cil-lock-locked" />
                      </CInputGroupText>
                      <CFormInput type="password" placeholder="Password" autocomplete="new-password"
                        v-model.trim="password" />
                    </CInputGroup>
                    <CInputGroup class="mb-4">
                      <CInputGroupText>
                        <CIcon icon="cil-lock-locked" />
                      </CInputGroupText>
                      <CFormInput type="password" placeholder="Repeat password" autocomplete="new-password"
                        v-model.trim="newpassword" />
                    </CInputGroup>
                  </div>
                  <div class="col-sm-6">
                    <CInputGroup class="mb-3">
                      <CInputGroupText>
                        <CIcon icon="cil-user" />
                      </CInputGroupText>
                      <CFormInput placeholder="First name" autocomplete="LastName" />
                    </CInputGroup>
                    <CInputGroup class="mb-3">
                      <CInputGroupText>
                        <CIcon icon="cil-user" />
                      </CInputGroupText>
                      <CFormInput placeholder="Last Name" autocomplete="LastName" />
                    </CInputGroup>
                    <CInputGroup class="mb-3">
                      <CInputGroupText>
                        <CIcon icon="cil-phone" />
                      </CInputGroupText>
                      <CFormInput placeholder="0743 793 901" autocomplete="username" type="tel"
                        v-model="rawPhoneNumber" />
                    </CInputGroup>
                  </div>
                </div>
                <div class="d-grid">
                  <CButton color="success" type="submit">Create Account</CButton>
                </div>
              </CForm>
            </CCardBody>
            <div class="col">
              <ul class="text-danger" v-if="errors">{{ errors.error }}
                <li v-for="e in errors" :key="e">{{ e }}</li>
              </ul>
            </div>
          </CCard>
        </CCol>
      </CRow>
    </CContainer>
  </div>
</template>

<script>
import { ref, watch } from 'vue';
import signupvalidations from '../../../services/signupvalidations';
import { useStore } from 'vuex';
import Swal from 'sweetalert2';

export default {
  name: 'Register',
  setup() {
    const rawPhoneNumber = ref('');
    const formattedPhoneNumber = ref('');
    const errors = ref({})
    const username = ref('')
    const email = ref('')
    const first_name = ref('')
    const last_name = ref('')
    const password = ref('')
    const newpassword = ref('')

    const store = useStore();

    // Watch for changes in rawPhoneNumber and format it
    watch(rawPhoneNumber, (newValue) => {
      // Strip all non-numeric characters from the phone number
      const strippedPhoneNumber = newValue.replace(/[^\d]/g, '');
      // Format the phone number with parentheses and hyphens
      if (strippedPhoneNumber.length === 10) {
        const formatted = `(${strippedPhoneNumber.slice(0, 3)}) ${strippedPhoneNumber.slice(3, 6)}-${strippedPhoneNumber.slice(6)}`;
        rawPhoneNumber.value = formatted;
        console.log(formattedPhoneNumber)

      } else {
        rawPhoneNumber.value = strippedPhoneNumber;
        console.log(formattedPhoneNumber)
      }
    });

    const register = () => {
      errors.value = new signupvalidations(email.value, password.value, newpassword.value, rawPhoneNumber.value).checkValidations();
      console.log(errors.value)
      if (errors.value.length) {
        Swal.fire(errors.value);
        return;
      }
      store.dispatch("auth/[actions] siggnup", {
        username: username.value,
        email: email.value,
        first_name: first_name.value,
        last_name: last_name.value,
        phone: rawPhoneNumber.value,
        password: password.value
      })
    }

    return {
      formattedPhoneNumber,
      rawPhoneNumber,
      errors,
      email,
      password,
      username,
      first_name,
      last_name,
      register,
    };
  }

}
</script>
