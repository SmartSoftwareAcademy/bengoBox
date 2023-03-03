<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Swal from "sweetalert2";
//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";
import reportdet from "@/components/report/header";
import axios from "axios";

export default {
  page: {
    title: "Password Policy",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  components: {
    Layout,
    PageHeader,
    reportdet,
  },
  data() {
    return {
      title: "Password Policy",
      id: 1,
      app_capital: false,
      app_small: false,
      app_char: false,
      app_numbers: false,
      app_min_char: 0,
      app_day_exp: 0,
      app_logoff_period: 0,
      changedpasspolicy: false,

      addcargo: false,
      editcargo: false,
      deletecargo: false,
      viewCargo: false,
      clickedPrintpdf: "",
      printedpdf: true,
      generatedExel: "",
      printReport: false,
      filteredValues: "",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Cargo List",
          active: true,
        },
      ],
      orderDatanew: null,
      pl: "",
      orderData: [
        //     {
        //       id: "1",
        //       screenname: "TEST",
        //       active: 0,
        //     },
      ],

      errors: "",
      headers: null,
      uniqueCars: null,
      showme: true,
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [
        1,
        2,
        5,
        10,
        25,
        50,
        100,
        200,
        300,
        500,
        1000,
        2000,
        3000,
        4000,
        5000,
        6000,
        9000,
        10000,
        20000,
      ],
      roadauthoritys: ["KeNHA", "KURA"],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: false,
      cargo_name: "",
      roadauthority: "",
      prefix: "",
      myindex: "",
      editmode: false,
      modaltitle: "Add",
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "cargo_name",
          label: "Cargo Name",
          sortable: true,
        },

        "action",
      ],
    };
  },
  watch: {
    viewCargo(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Opened Cargo list:${newValue}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    printReport() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Cargo reports:${this.clickedPrintpdf},${this.generatedExel},${this.filteredValues}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    addcargo(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Add cargo:${newValue},value:${this.cargo_name}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    editcargo(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Edit cargo:${newValue},value:${this.cargo_name}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    deletecargo(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Delete cargo:${newValue},cargo id:${this.id}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
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
    this.viewCargo = true;
    // Set the initial number of items
    this.updatearray();
    this.totalRows = this.items.length;
  },
  methods: {
    updatearray() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      axios
        .get(window.$http + `AccessPassPolicy`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          //alert(this.orderData);
          //console.log(this.lists);
          //alert(this.orderData[0].app_capital);
          if (this.orderData.length > 0) {
            //alert(this.orderData);
            //console.log(this.lists);
            this.app_capital = false;
            if (this.orderData[0].app_capital == 1) {
              this.app_capital = true;
            }
            this.app_numbers = false;

            if (this.orderData[0].app_numbers == 1) {
              // alert(this.orderData[0].app_numbers);
              this.app_numbers = true;
            }
            this.app_small = false;
            if (this.orderData[0].app_small == 1) {
              this.app_small = true;
            }
            this.app_char = false;
            if (this.orderData[0].app_char == 1) {
              this.app_char = true;
            }

            this.app_min_char = this.orderData[0].app_min_char;
            this.app_day_exp = this.orderData[0].app_day_exp;
            this.app_logoff_period = this.orderData[0].app_logoff_period;
          }
          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    saverecs() {
      //console.log("Error on submit");
      var app_capital = 0;
      var app_small = 0;
      var app_char = 0;
      var app_numbers = 0;
      var shoulhave = " Password Should have <BR> ";
      if (this.app_capital == true) {
        app_capital = 1;
        shoulhave += "Capital Letters <BR>";
      }

      if (this.app_numbers == true) {
        // alert(this.orderData[0].app_numbers);
        app_numbers = 1;
        shoulhave += " Numbers <BR>";
      }

      if (this.app_small == true) {
        app_small = 1;
        shoulhave += " Small Letters <BR>";
      }

      if (this.app_char == true) {
        app_char = 1;
        shoulhave += " Symbols <BR>";
      }

      var str =
        "<div> Automatic Log Off:" +
        this.app_logoff_period +
        "(in Seconds)</div>" +
        "Password Expiry:" +
        this.app_day_exp +
        " in Days <br>" +
        "Password Length:  " +
        this.app_min_char +
        "(Number of characters)<br>" +
        shoulhave;
      Swal.fire({
        title: "Do you want to save password policy \n" + "?",
        html: "<pre>" + str + "</pre>",
        icon: "info",
        showCancelButton: true,
        confirmButtonColor: "#006400",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Save it!",
      }).then((result) => {
        if (result.value) {
          //alert(app_capital);
          const data = {
            id: this.id,
            app_capital: app_capital,
            app_small: app_small,
            app_char: app_char,
            app_numbers: app_numbers,
            app_min_char: this.app_min_char,
            app_day_exp: this.app_day_exp,
            app_logoff_period: this.app_logoff_period,
          };
          console.log(data);

          axios
            .put(window.$http + `AccessPassPolicy/` + this.id, data, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              this.changedpasspolicy = true;
              console.log(response.data);
              //if (response.data.trim() != "") {
              Swal.fire({
                position: "center",
                icon: "success",
                title: "Your work has been saved" + response.data,
                showConfirmButton: false,
                timer: 1500,
              }).then(() => {
                //this.clearvalues();
                this.updatearray();
              });
              //}
            })
            .catch((e) => {
              Swal.fire({
                position: "center",
                icon: "error",
                title: "" + e,
                showConfirmButton: true,
              }).then(() => {
                Swal.close();
              });
            });
        }
      });
    },

    /**
     * Export to CSV
     */
    getrpt(p_excel) {
      this.printReport = true;
      p_excel = "Generated an excel document";
      this.generatedExel = p_excel;
      //alert(new Date());
      const d = new Date();
      const year = d.getFullYear();
      const month = d.getMonth();
      const date = d.getDate();

      const hour = d.getHours();
      const min = d.getMinutes();
      const sec = d.getSeconds();
      const msec = d.getMilliseconds();
      const filename =
        year +
        "-" +
        month +
        "-" +
        date +
        "-" +
        hour +
        "-" +
        min +
        "-" +
        sec +
        "-" +
        msec;
      //alert(filename);
      const data = this.orderData.map((row) => ({
        ID: row.id,
        Cargo_Name: row.cargo_name,
      }));
      //alert("");
      const csvRows = [];
      //get headers
      const headers = Object.keys(data[0]);

      csvRows.push(headers.join(","));
      //alert(csvRows);
      //loop over the headers
      for (const row of data) {
        const values = headers.map((header) => {
          const escaped = ("" + row[header]).replace(/"/g, '\\"');
          // alert(escaped);
          return '"' + escaped + '"'; //'" + escaped + "';
        });
        csvRows.push(values.join(","));
      }
      //alert(csvData);
      const csvData = csvRows.join("\n");
      //alert(csvData);

      const blob = new Blob([csvData], { type: "textcsv" });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement("a");
      a.setAttribute("hidden", "");
      a.setAttribute("href", url);
      a.setAttribute("download", this.title + filename + ".csv");
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
    },

    /**
     * Search the table data with search input
     */
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
      this.printReport = true;
      this.filteredValues = this.filter;
    },
    handleSubmit() {
      console.log("Error on submit");
    },
    printpdf(pl, p_pdf) {
      this.printReport = true;
      p_pdf = "Clicked print pdf button";
      this.clickedPrintpdf = p_pdf;
      this.pl = pl;
      const data = this.orderData.map((row) => ({
        ID: row.id,
        Cargo_Name: row.screenname,
      }));

      //get headers
      const headers = Object.keys(data[0]);
      const cars = [];
      Object.entries(data).forEach((val) => {
        const [key, value] = val;
        console.log(key, value);
        cars.push(Object.values(data[key]));
      });

      const uniqueCars = Array.from(new Set(cars));
      this.headers = headers;
      this.uniqueCars = uniqueCars;
      //alert(headers);
    },
    edit(index, myid, cargo_name) {
      //alert(rolename);
      this.editmode = true;
      this.modaltitle = "Edit Cargos";
      this.id = myid;
      this.cargo_name = cargo_name;
      this.myindex = index;
      //this.focusInput();

      //this.$refs.search.focus();
      //$("#search").focus();
    },
    editrec() {
      if (this.cargo_name.trim() == "") {
        Swal.fire("Please enter  Cargo Name!");
        return;
      }

      //alert(this.myindex);

      const data = {
        id: this.id,
        cargo_name: this.cargo_name,
        active: 0,
      };
      //alert(this.screenname);
      axios
        .put(window.$http + `Cargos/` + this.id, data, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData[this.myindex].id = this.id;
          this.orderData[this.myindex].cargo_name = this.cargo_name;
          this.orderData[this.myindex].active = this.active;
          console.log(response.data);
          this.editcargo = true;
          if (response.data.trim() != "") {
            Swal.fire({
              position: "center",
              icon: "success",
              title: "Your work has been saved" + response.data,
              showConfirmButton: false,
              timer: 1500,
            });
          }
        })
        .catch((e) => {
          Swal.fire("" + e);
          //this.errors.push(e);
        });

      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
    position() {
      if (this.cargo_name.trim() == "") {
        Swal.fire("Please enter  Cargos Name!");
        return;
      }

      var orderid = this.orderData.length + 1;
      this.orderData.push({
        id: orderid,
        cargo_name: this.cargo_name,
      });
      const data = {
        cargo_name: this.cargo_name,
        active: 0,
      };
      axios
        .post(window.$http + `Cargos/`, data, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data);
          this.addcargo = true;
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
        });

      this.screenname = "";
    },
    deleterec(index, id, cargo_name) {
      //alert(rolename);
      this.id = id;
      this.cargo_name = cargo_name;
      Swal.fire({
        title: "Are you sure, you want to delete " + this.cargo_name + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          axios
            .delete(window.$http + `Cargos/` + this.id, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.deletecargo = true;
              this.orderData.splice(index, 1);
              Swal.fire(
                this.cargo_name + " Deleted!",
                "Your record has been deleted.",
                "success"
              );
            })
            .catch((e) => {
              Swal.fire("" + e);
            });
        }
      });
    },
    clearvalues() {
      this.name = "";
      this.editmode = false;
      this.modaltitle = "Add Cargos";
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div class="card">
      <div class="card-body">
        <form @submit.prevent="updatearray()">
          <div class="row">
            <div class="col-sm-6">
              <b-card-title>
                <h5 class="card-title mb-3">Password formats</h5>
              </b-card-title>
              <div class="col-sm-12 mb-3">
                <div class="input-group">
                  <div class="input-group-text col-sm-3">Password Length:</div>
                  <input
                    class="form-control"
                    type="text"
                    placeholder="8"
                    v-model="app_min_char"
                  />
                  <div class="input-group-text col-sm-3">
                    (Characters length)
                  </div>
                </div>
              </div>

              <div class="col-sm-12 mb-3">
                <div class="input-group">
                  <div class="input-group-text col-sm-3">Password Expiry:</div>
                  <input
                    class="form-control"
                    type="text"
                    placeholder="90"
                    v-model="app_day_exp"
                  />

                  <div class="input-group-text col-sm-3">(In Days)</div>
                </div>
              </div>

              <div class="col-sm-12">
                <div class="input-group">
                  <div class="input-group-text col-sm-3">
                    Automatic Log Off:
                  </div>
                  <input
                    class="form-control"
                    type="text"
                    placeholder="1000"
                    v-model="app_logoff_period"
                  />
                  <div class="input-group-text col-sm-3">(In Second(s))</div>
                </div>
              </div>
            </div>

            <div class="col-sm-4 m-4">
              <b-card-title>
                <h5 class="card-title">Password Format Includes;</h5>
              </b-card-title>
              <div>
                <input type="checkbox" value="email" v-model="app_capital" />
                <label class="m-2"> Capital Letters </label>
              </div>

              <div>
                <input type="checkbox" value="sms" v-model="app_small" />
                <label class="m-2">Small Letters</label>
              </div>

              <div>
                <input type="checkbox" value="email" v-model="app_numbers" />
                <label class="m-2"> Numbers </label>
              </div>

              <div>
                <input type="checkbox" value="sms" v-model="app_char" />
                <label class="m-2"> Character </label>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-sm-8">
              <b-button variant="dark" @click="saverecs()"
                >Set Password Policy</b-button
              >
            </div>
          </div>
        </form>
      </div>
    </div>
    <b-modal id="modal-Print" title="Print PDF" hide-footer size="bg" centered>
      <reportdet
        :title="title"
        :orderData="orderData"
        :pl="pl"
        :headers="headers"
        :uniqueCars="uniqueCars"
        :shome="showme"
        v-show="showme"
      ></reportdet>
    </b-modal>
  </Layout>
</template>
//#print { //height: 11in; //width: 8.5in; //}
<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
</style>