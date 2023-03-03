<script>
import { required, email } from "vuelidate/lib/validators";
import appConfig from "@/app.config";

/**
 * Register component
 */
export default {
  page: {
    title: "Register",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  data() {
    return {
      user: {
        username: "",
        email: "",
        password: "",
      },
      submitted: false,
      tryingToRegister: false,
      isRegisterError: false,
      title: "Register",
    };
  },
  validations: {
    user: {
      username: {
        required,
      },
      email: {
        required,
        email,
      },
      password: {
        required,
      },
    },
  },
  computed: {
    notification() {
      return this.$store ? this.$store.state.notification : null;
    },
    notificationAutoCloseDuration() {
      return this.$store && this.$store.state.notification ? 5 : 0;
    },
  },
  mounted() {
    document.body.classList.add("authentication-bg");
  },
  methods: {
    // Try to register the user in with the email, username
    // and password they provided.
    tryToRegisterIn() {
      this.submitted = true;
      // stop here if form is invalid
      this.$v.$touch();

      if (this.$v.$invalid) {
        return;
      } else {
        if (process.env.VUE_APP_DEFAULT_AUTH === "firebase") {
          this.tryingToRegister = true;
          // Reset the regError if it existed.
          this.regError = null;
          return (
            this.$store
              .dispatch("auth/register", {
                email: this.user.email,
                password: this.user.password,
              })
              // eslint-disable-next-line no-unused-vars
              .then((token) => {
                this.tryingToRegister = false;
                this.isRegisterError = false;
                this.registerSuccess = true;
                if (this.registerSuccess) {
                  this.$router.push(
                    this.$route.query.redirectFrom || {
                      path: "/",
                    }
                  );
                }
              })
              .catch((error) => {
                this.tryingToRegister = false;
                this.regError = error ? error : "";
                this.isRegisterError = true;
              })
          );
        } else if (process.env.VUE_APP_DEFAULT_AUTH === "bengoboxAuth") {
          const { email, username, password } = this.user;
          if (email && username && password) {
            this.$store.dispatch("bengoboxAuth/registeruser", this.user);
          }
        }
      }
    },
  },
};
</script>

<template>
  <div>

    <div class="account-pages my-5 pt-sm-5">
      <div class="container">
        <div class="row">
          <div class="col-lg-12">
            <div class="text-center">
              <router-link to="/" class="mb-5 d-block auth-logo">
                <img src="@/assets/images/logo.png" alt width="350" class="logo" />
              </router-link>
            </div>
          </div>
        </div>
        <div class="row align-items-center justify-content-center">
          <div class="col-md-8 col-lg-6 col-xl-5">
            <div class="card">
              <div class="card-body p-4" style="background-color: yellow">
                <div class="text-center mt-2">
                  <h5 class="text-primary">Request Account</h5>
                </div>
                <div class="p-2 mt-4">
                  <div v-if="notification.message" :class="'alert ' + notification.type">
                    {{ notification.message }}
                  </div>

                  <b-form @submit.prevent="tryToRegisterIn">
                    <b-form-group id="email-group" label="Username" class="mb-3" label-for="username">
                      <b-form-input id="username" v-model="user.username" type="text" placeholder="Enter username"
                        :class="{
                          'is-invalid': submitted && $v.user.username.$error,
                        }" style="
                          color: black;
                          background: transparent;
                          border: solid 1px black;
                        "></b-form-input>
                      <div v-if="submitted && !$v.user.username.required" class="invalid-feedback">
                        Username is required.
                      </div>
                    </b-form-group>

                    <b-form-group id="fullname-group" label="Email" label-for="email" class="mb-3">
                      <b-form-input id="email" v-model="user.email" type="email" placeholder="Enter email" :class="{
                        'is-invalid': submitted && $v.user.email.$error,
                      }" style="
                          color: black;
                          background: transparent;
                          border: solid 1px black;
                        "></b-form-input>
                      <div v-if="submitted && $v.user.email.$error" class="invalid-feedback">
                        <span v-if="!$v.user.email.required">Email is required.</span>
                        <span v-if="!$v.user.email.email">Please enter valid email.</span>
                      </div>
                    </b-form-group>

                    <b-form-group id="password-group" label="Password" class="mb-3" label-for="password">
                      <b-form-input id="password" v-model="user.password" type="password" placeholder="Enter password"
                        :class="{
                          'is-invalid': submitted && $v.user.password.$error,
                        }" style="
                          color: black;
                          background: transparent;
                          border: solid 1px black;
                        "></b-form-input>
                      <div v-if="submitted && !$v.user.password.required" class="invalid-feedback">
                        Password is required.
                      </div>
                    </b-form-group>

                    <div class="mt-3 text-end">
                      <b-button type="submit" variant="primary" class="w-sm">Request Account</b-button>
                    </div>

                    <div class="mt-4 text-center">
                      <p class="text-muted mb-0">
                        Already have an account ?
                        <router-link to="/login" class="fw-medium text-primary">Login</router-link>
                      </p>
                    </div>
                  </b-form>
                </div>
                <!-- end card-body -->
              </div>
              <!-- end card -->
            </div>
            <div class="mt-5 text-center text-white">
              <p>© {{ new Date().getFullYear() }} kenloadv2</p>
            </div>
          </div>
          <!-- end col -->
        </div>
      </div>
    </div>
    <!-- end row -->
  </div>
</template>

<style lang="scss" module>
input:-webkit-autofill,
input:-webkit-autofill:hover,
input:-webkit-autofill:focus,
input:-webkit-autofill:active {
  transition: background-color 5000s;
  -webkit-text-fill-color: black !important;
}
</style>