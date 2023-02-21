<template>
    <div>
    <div class="home-btn d-none d-sm-block">
      <router-link to="/" class="text-dark"
        ><i class="mdi mdi-home-variant h2"></i
      ></router-link>
    </div>
    <div class="account-pages my-5 pt-sm-5">
      <div class="container">
        <div class="row justify-content-center">
          <div class="col-md-8 col-lg-6 col-xl-5">
            <div>
             
              <div class="card">
                <div class="card-body p-4">
                  <div class="text-center mt-2">
                    <h5 class="text-primary">Reset Password</h5>
                    <p class="text-muted">Reset Password with KenLoad.</p>
                  </div>
                  <div class="p-2 mt-4">
                    <div
                      class="alert alert-success text-center mb-4"
                      role="alert"
                    >
                      Enter your Email and instructions will be sent to you!
                    </div>
                    <form>
                      <div class="mb-3">
                        <label for="useremail">Email</label>
                        <input
                          type="email"
                          class="form-control"
                          id="useremail"
                          placeholder="Enter email"
                        />
                      </div>

                      <div class="mt-3 text-end">
                          <router-link to="/">
                        <button
                          class="btn btn-primary w-sm waves-effect waves-light"
                          
                        >
                          Reset
                        </button>
                        </router-link>
                      </div>

                      <div class="mt-4 text-center">
                        <p class="mb-0">
                          Remember It ?
                            
                          <router-link
                            to="/"
                            class="fw-medium text-primary"
                            >Signin</router-link
                          >
                        </p>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
              <div class="mt-5 text-center">
                <p>
                  © {{ new Date().getFullYear() }} KenLoad. Crafted with
                  <i class="mdi mdi-heart text-danger"></i> For KeNHA
                </p>
              </div>
            </div>
          </div>
        </div>
        <!-- end row -->
      </div>
      <!-- end container -->
    </div>
  </div>

</template>

<script>
export default {
method:{
     handleSubmit(){
            console.log('Error on submit'); 
        },
},
}
</script>

<style>


</style>