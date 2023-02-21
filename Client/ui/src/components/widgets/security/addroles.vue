<script>
import Swal from "sweetalert2";
import axios from "axios";
//var CryptoJS = require("crypto-js");

//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";

export default {
  props: {
    selectedlists: Array,
    lists: Array,
    orderData: Array,
  },
  components: {},
  data() {
    return {
      title: "",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Role List",
          active: true,
        },
      ],
      rname: "",
      description: "",
      //selectedlists: [],
      addroleslog: false,
      id: "",
      headers: null,
      uniqueCars: null,
      showme: true,
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [
        1, 2, 5, 10, 25, 50, 100, 200, 300, 500
      ],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: true,
      name: "",
      myindex: "",
      modaltitle: "Add",
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "name",
          label: "Role Name",
          sortable: true,
        },
      ],
      //lists: [],
    };
  },
  watch: {
    addroleslog(newValue) {
      var curentuser = JSON.parse(localStorage.user).email
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Add Role:${newValue},\nDetails(role name:${this.rname},\ndescription:${this.description},\nscreens[${this.selectedlists}])`,
        computer: window.localStorage.clientip
      }
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then(response => {
          console.log(response.data)
        })
        .catch(e => {
          console.log(e)
        })
    }
  },
  computed: {
    /**
     * Total no. of records
     */
    rows() {
      return this.orderData.length;
    },
  },
  mounted() {
    // Set the initial number of items
    this.focusInput();
    this.totalRows = this.items.length;
    this.fetchscreens();
  },
  methods: {
    fetchscreens() {
      //alert(window.$http);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      this.selectedlists.forEach((screen) => {
        this.lists.map((name) => {
          return [...this.lists, name !== screen]
        }).forEach((e) => {
          this.lists.push(e);
        })
      });
      //console.log(this.orderData);
      Swal.close();
    },
    focusInput() {
      this.$refs.rname.focus();
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

    /**
     * Search the table data with search input
     */
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    handleSubmit() {
      //alert(this.rname);
      //console.log("Error on submit");
    },
    add() {
      //var timenow = this.gettime();
      //console.log(timenow);
      if (this.rname.trim() == "") {
        Swal.fire("Please enter Role Name!", {
          className: "alert-custom",
        });
        return;
      }
      axios
        .post(window.$http + "UserRoles?roleName=" + this.rname, { roleName: this.rname }, { headers: window.$headers })
        .then((response) => {
          //this.screenlist = response;
          var screens = this.selectedlists.toString()
          var roleid = response.data.id;
          this.orderData = response.data;
          console.log(response.data);
          // this.addroleslog = true;
          if (response.status == 200) {
            axios
              .post(window.$http + "userrolescreens?roleid=" + roleid + "&screens=" + screens, {
                roleid: roleid, screens: screens
              }, { headers: window.$headers })
              .then(() => {
                Swal.fire({
                  position: "center",
                  icon: "success",
                  title: "Your work has been saved",
                  showConfirmButton: false,
                  timer: 1500,
                }).then((result) => {
                  result;
                  this.$root.$emit("bv::hide::modal", "modal-Add");
                  this.clearvalues();
                });
              });

          }
          console.log(response);
        })
        .catch(function (error) {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Error on submmission. Check Servers.",
            showConfirmButton: false,
            timer: 1500,
          }).then((result) => {
            result;
            this.clearvalues();
          });
          console.log(error);
        });
    },
    editrec() {
      if (this.rname.trim() == "") {
        Swal.fire("Please enter Role Name!");
        return;
      }

      //alert(this.myindex);
      this.orderData[this.myindex].id = this.id;
      this.orderData[this.myindex].name = this.rname;
      this.orderData[this.myindex].screens = this.screenlist;
      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
    deleterec(index, id, rname) {
      //alert(rolename);
      this.id = id;
      this.rname = rname;
      Swal.fire({
        title: "Are you sure, you want to delete " + this.rname + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          this.orderData.splice(index, 1);
          //this.$delete(this.orderData, this.id - 1);
          Swal.fire("Deleted!", this.name + " has been deleted.", "success");
        }
      });
    },
    clearvalues() {
      //alert();
      this.rname = "";
      this.selectedlists = []
      //this.allToLeft();
      //this.lists = [];
    },
    oneToRight() {
      // if (this.rname.trim() == "") {
      //   Swal.fire("Please enter Role Name!", {
      //     className: "alert-custom",
      //   });
      //   return;
      // }
      //console.log("allToRight");

      var select = document.getElementById("lists").value;
      //alert(select);
      //var res = select.split("-");

      //select = res[1];
      //var indexselect = res[0];
      if (select == undefined) {
        return;
      }

      //alert(select);
      if (select != "") {
        this.selectedlists.push(select);
        var del = this.lists.indexOf(select);
        //alert(del);
        this.lists.splice(del, 1);
      }
    },
    oneToLeft() {
      var select = document.getElementById("selectedlists").value;
      //var res = select.split("-");
      //select = res[1];
      //var indexselect = res[0];
      //alert("" + select);
      if (select == undefined) {
        return;
      }
      if (select != "") {
        this.lists.push(select);
        //this.lists.push(select);
        var del = this.selectedlists.indexOf(select);
        this.selectedlists.splice(del, 1);
        //this.selectedlists.splice(indexselect, 1);
      }
    },
    allToRight() {
      // alert(this.lists.length);
      //console.log("allToRight");
      var del = this.lists.length;
      if (this.lists[0] == undefined) {
        return;
      }
      //alert(del + "");
      for (var i = 0; i <= this.lists.length; i++) {
        //alert(i + "");
        this.selectedlists.push(this.lists[i]);
        //var del = this.lists.indexOf(this.lists[i]);
      }
      this.lists.splice(0, del);
    },
    allToLeft() {
      if (this.selectedlists[0] == undefined) {
        return;
      }

      var del = this.selectedlists.length;
      for (var i = 0; i < this.selectedlists.length; i++) {
        this.lists.push(this.selectedlists[i]);
        //  var del = this.selectedlists.indexOf(this.selectedlists[i]);
        //this.selectedlists.splice(del, 1);
      }
      this.selectedlists.splice(0, del);
    },
    populate() {
      //alert("");
      if (this.selectedlists.length <= 0) {
        Swal.fire({ title: "Please select a screen!" });
      }
      if (this.rname == "") {
        Swal.fire({ title: "Please enter role name!" });
      }

      if (this.description == "") {
        Swal.fire({ title: "Please enter description!" });
      }
      //this.selectedlists = this.lists;
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="[editmode ? 'Edit Roles' : 'Add Roles']" :items="items" />
    <div>
      <form @submit.prevent="handleSubmit()">
        <div class="card">
          <div class="card-body">
            <div class="card">
              <div class="card-body">
                <div class="col-sm-12">
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">Role Name:</div>
                    <input :disabled="editmode" ref="rname" class="form-control" placeholder="role Name"
                      v-model="rname" />
                  </div>
                </div>

                <div class="mt-3"></div>
                <div class="row">
                  <div class="col-sm">
                    <div class="">Select Screens / Windows / Page:</div>

                    <select class="form-control" size="15" id="lists" multiple>
                      <option :id="index" :value="list" v-for="(list, index) in lists" :key="list">
                        {{ list }}
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
                    <select class="form-control" size="15" id="selectedlists" multiple>
                      <option :id="index" v-for="(selectedlist, index) in selectedlists" :value="selectedlist"
                        :key="selectedlist">
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
                <button v-show="!editmode" class="btn btn-primary" @click="add()">
                  Add Role
                </button>
                <button v-show="editmode" class="btn btn-primary" @click="edit()">
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