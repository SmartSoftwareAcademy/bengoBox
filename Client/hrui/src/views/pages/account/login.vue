<script>
import { required, email } from "vuelidate/lib/validators";
import appConfig from "@/app.config";

/**
 * Login component
 */
export default {
  page: {
    title: "Login",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  data() {
    return {
      email: "",
      password: "",
      submitted: false,
      authError: null,
      tryingToLogIn: false,
      isAuthError: false,
    };
  },
  validations: {
    email: {
      required,
      email,
    },
    password: {
      required,
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
    for (var i = 0; i < localStorage.length; i++) {
      if (localStorage.key(i).toString() === "token") {
        console.log("");
      }
    }
  },
  methods: {
    // Try to log the user in with the username
    // and password they provided.
    tryToLogIn() {
      this.submitted = true;
      // stop here if form is invalid
      this.email = this.email.trim();
      this.$v.$touch();

      if (this.$v.$invalid) {
        return;
      } else {
        if (process.env.VUE_APP_DEFAULT_AUTH === "firebase") {
          /*this.tryingToLogIn = true;
          // Reset the authError if it existed.
          this.authError = null;
          return (
            this.$store
              .dispatch("auth/logIn", {
                email: this.email,
                password: this.password,
              })
              // eslint-disable-next-line no-unused-vars
              .then((token) => {
                this.tryingToLogIn = false;
                this.isAuthError = false;
                // Redirect to the originally requested page, or to the home page
                this.$router.push(
                  this.$route.query.redirectFrom || {
                    path: "/",
                  }
                );
              })
              .catch((error) => {
                this.tryingToLogIn = false;
                this.authError = error ? error : "";
                this.isAuthError = true;
              })
          );*/
        } else if (process.env.VUE_APP_DEFAULT_AUTH === "bengoboxAuth") {
          const { email, password } = this;
          if (email && password) {
            this.tryingToLogIn = true;
            this.$store.dispatch("authfack/login", {
              email,
              password,
            });
          }
        }
      }
    },
  },
};
</script>

<template>
  <div>
    <div class="account-pages">
      <div class="container">
        <div class="row">
          <div class="col-lg-12">
            <div class="text-center">
              <router-link to="/" class="d-block auth-logo">
                <img src="@/assets/images/logo.png" alt height="350" class="logo" />
              </router-link>
            </div>
          </div>
        </div>
        <div class="row align-items-center justify-content-center">
          <div class="col-md-8 col-lg-6 col-xl-5">
            <div class="card">
              <div class="card-body p-4" style="background-color: orange">
                <div class="text-center mt-2">
                  <h5 class="text-success">
                    Welcome...! Please Login To Continue
                  </h5>
                </div>
                <div class="p-2 mt-4">
                  <b-alert v-model="isAuthError" variant="danger" class="mt-3" dismissible>{{ authError }}</b-alert>

                  <div v-if="notification.message" :class="'alert ' + notification.type">
                    {{ notification.message }}
                  </div>

                  <b-form @submit.prevent="tryToLogIn">
                    <b-form-group id="input-group-1" class="mb-3" label="Email" label-for="input-1">
                      <b-form-input id="input-1" v-model="email" type="text" placeholder="Enter email"
                        :class="{ 'is-invalid': submitted && $v.email.$error }" style="
                          color: black;
                          background: transparent;
                          border: solid 1px black;
                        "></b-form-input>
                      <div v-if="submitted && $v.email.$error" class="invalid-feedback">
                        <span v-if="!$v.email.required">Email is required.</span>
                        <span v-if="!$v.email.email">Please enter valid email.</span>
                      </div>
                    </b-form-group>

                    <b-form-group id="input-group-2" class="mb-3">
                      <label for="input-2">Password</label>
                      <b-form-input id="input-2" v-model="password" type="password" placeholder="Enter password" :class="{
                        'is-invalid': submitted && $v.password.$error,
                      }" style="
                          color: black;
                          background: transparent;
                          border: solid 1px black;
                        "></b-form-input>
                      <div v-if="submitted && !$v.password.required" class="invalid-feedback">
                        Password is required.
                      </div>
                    </b-form-group>
                    <div class="form-check">
                      <input type="checkbox" class="form-check-input" id="auth-remember-check" />
                      <label class="form-check-label" for="auth-remember-check">Remember me</label>
                      <div class="float-end">
                        <router-link to="/forgot-password" class="text-success">Forgot password?</router-link>
                      </div>
                    </div>
                    <div class="mt-3 text-end">
                      <b-button type="submit" variant="primary" class="w-sm">Log In</b-button>
                    </div>

                    <div class="mt-4 text-center">
                      <div class="signin-other-title">
                        <h5 class="font-size-14 mb-3 title">Sign in with</h5>
                      </div>

                      <ul class="list-inline">
                        <li class="list-inline-item">
                          <a href="javascript:void()" class="social-list-item bg-primary text-white border-primary">
                            <i class="mdi mdi-facebook"></i>
                          </a>
                        </li>
                        <li class="list-inline-item">
                          <a href="javascript:void()" class="social-list-item bg-info text-white border-info">
                            <i class="mdi mdi-twitter"></i>
                          </a>
                        </li>
                        <li class="list-inline-item">
                          <a href="javascript:void()" class="social-list-item bg-danger text-white border-danger">
                            <i class="mdi mdi-google"></i>
                          </a>
                        </li>
                      </ul>
                    </div>

                    <div class="mt-4 text-center">
                      <p class="mb-0">
                        Don't have an account ? Get Started
                        <router-link to="/register" class="fw-medium text-success">Here</router-link>
                      </p>
                    </div>
                  </b-form>
                </div>
                <!-- end card-body -->
              </div>
              <!-- end card -->
            </div>
            <div class="mt-5 text-center">
              <p>TDBSoft © {{ new Date().getFullYear() }}. Crafted with
                <i class="mdi mdi-heart text-danger"></i> by TDBSoft
              </p>
            </div>
            <!-- end row -->
          </div>
          <!-- end col -->
        </div>
        <!-- end row -->
      </div>
    </div>
  </div>
</template>

<style>
h2 {
  color: black;
  margin: 0;
  font: 0.8em verdana;
  text-transform: uppercase;
  letter-spacing: 0.1em;
}

.loading {
  text-align: center;
}

.loading span {
  display: inline-block;
  vertical-align: middle;

  width: 0.6em;
  height: 0.6em;
  margin: 0.19em;
  background: black;
  border-radius: 0.6em;
  animation: loading 1s infinite alternate;
}

/*
 * Dots Colors
 * Smarter targeting vs nth-of-type?
 */
.loading span:nth-of-type(2) {
  background: black;
  animation-delay: 0.2s;
}

.loading span:nth-of-type(3) {
  background: black;
  animation-delay: 0.4s;
}

.loading span:nth-of-type(4) {
  background: black;
  animation-delay: 0.6s;
}

.loading span:nth-of-type(5) {
  background: black;
  animation-delay: 0.8s;
}

.loading span:nth-of-type(6) {
  background: black;
  animation-delay: 1s;
}

.loading span:nth-of-type(7) {
  background: black;
  animation-delay: 1.2s;
}

/*
 * Animation keyframes
 * Use transition opacity instead of keyframes?
 */
@keyframes loading {
  0% {
    opacity: 0;
  }

  100% {
    opacity: 1;
  }
}
</style>
<style lang="scss" module>
input:-webkit-autofill,
input:-webkit-autofill:hover,
input:-webkit-autofill:focus,
input:-webkit-autofill:active {
  transition: background-color 5000s;
  -webkit-text-fill-color: black !important;
}
</style>
