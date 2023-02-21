<script>
//import Swal from "sweetalert2";
import axios from "axios";
//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";

export default {
  props: {
    desc: String,
    screen: String,
  },
  components: {},
  data() {
    return {};
  },
  computed: {},
  mounted() {},
  methods: {
    auditlog() {
      //alert(window.$http);
      var datetime = this.gettime();

      const data = {
        datetime: datetime,
        useremail: this.useremail,
        screen: this.screen,
        desc: this.desc,
        print: 0,
        comp: "",
      };
      axios
        .post(window.$http + `auditlogs/`, data)
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data);
        })
        .catch((e) => {
          Swal.fire("" + e);
        });
    },
    gettime() {
      //2021-06-11T09:05:53.07
      //2021-5-11T12:54.48
      var DateNow = new Date();
      var DateNowString;
      DateNow.setDate(DateNow.getDate() + 20);
      DateNowString =
        DateNow.getFullYear() +
        "-" +
        ("0" + (DateNow.getMonth() + 1)).slice(-2) +
        "-" +
        ("0" + DateNow.getDate()).slice(-2) +
        "T" +
        ("0" + DateNow.getHours()).slice(-2) +
        ":" +
        ("0" + DateNow.getMinutes()).slice(-2) +
        ":" +
        ("0" + DateNow.getSeconds()).slice(-2) +
        "." +
        ("0" + DateNow.getMilliseconds()).slice(-2);
      return DateNowString;
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div>
      <form @submit.prevent="handleSubmit">
        <div class="card">
          <div class="card-body">
            <div class="card">
              <div class="card-body">
                <div class="col-sm-12">
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">Role Name:</div>
                    <input
                      :disabled="editmode"
                      ref="rname"
                      class="form-control"
                      type="text"
                      placeholder="role Name"
                      v-model="rname"
                    />
                  </div>
                </div>

                <div class="col-sm-12">
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">
                      Role Description:
                    </div>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Description"
                      v-model="description"
                    />
                  </div>
                </div>
                <div class="mt-3"></div>
                <div class="row">
                  <div class="col-sm">
                    <div class="">Select Screens / Windows / Page:</div>

                    <select class="form-control" size="15" id="lists" multiple>
                      <option
                        :id="index"
                        :value="list.screenname"
                        v-for="(list, index) in lists"
                        :key="list"
                      >
                        {{ list.screenname }}
                      </option>
                    </select>
                  </div>
                  <div class="col-sm">
                    <div class="row mb-4"></div>
                    <div class="row mb-4"></div>
                    <div class="row mb-4"></div>
                    <div class="row mb-2">
                      <button class="btn btn-secondary" @click="allToRight()">
                        Add All &raquo;
                      </button>
                    </div>

                    <div class="row mb-4">
                      <button class="btn btn-secondary" @click="oneToRight()">
                        Add Selected&rsaquo;
                      </button>
                    </div>
                    <div class="row mb-4"></div>
                    <div class="row mb-2">
                      <button class="btn btn-danger" @click="oneToLeft()">
                        &lsaquo; Remove Selected
                      </button>
                    </div>
                    <div class="row">
                      <button class="btn btn-danger" @click="allToLeft()">
                        &laquo; Remove All
                      </button>
                    </div>
                  </div>

                  <div class="col-sm">
                    <div>Selected Screens / Windows / Page:</div>
                    <select
                      class="form-control"
                      size="15"
                      id="selectedlists"
                      multiple
                    >
                      <option
                        :id="index"
                        v-for="(selectedlist, index) in selectedlists"
                        :value="selectedlist"
                        :key="selectedlist"
                      >
                        {{ selectedlist }}
                      </option>
                    </select>
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-sm-10"></div>
              <div class="col-sm-2">
                <button
                  v-show="!editmode"
                  class="btn btn-primary"
                  @click="add()"
                >
                  Add Role
                </button>
                <button
                  v-show="editmode"
                  class="btn btn-primary"
                  @click="edit()"
                >
                  Edit Role
                </button>
              </div>
            </div>
          </div>
        </div>
      </form>
    </div>
  </Layout>
</template>
//#print {
  //height: 11in;
  //width: 8.5in;
//}
<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
.alert-custom {
  background-color: yellow;
}
.swal-overlay {
  background-color: rgba(43, 165, 137, 0.45);
}
.swal-modal {
  background-color: rgba(63, 255, 106, 0.69);
  border: 3px solid white;
}
</style>