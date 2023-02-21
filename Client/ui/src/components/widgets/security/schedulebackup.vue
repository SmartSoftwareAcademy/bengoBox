<script>
import Swal from "sweetalert2";
//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";
import reportdet from "@/components/report/header";
import Multiselect from "vue-multiselect";
import axios from "axios";

export default {
  components: {
    reportdet,
    Multiselect,
  },
  props: {
    title: String,
    items: Array,
  },
  data() {
    return {
      filefolderpath: "E:\\Kenloadimg\\kenload",
      pl: "",
      headers: null,
      uniqueCars: null,
      showme: true,
      bkPath: "E:\\Kenloadimg\\kenload",
      backupName: "kenloadv2Manualbackup",
      bkname: "kenloadv2Autobackup",
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      //
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
      backupTime: new Date().getTime(),
      backupDay: "Monday",
      Days: [
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sunday",
      ],
      backup_m: 0,
      backup_t: 0,
      backup_w: 0,
      backup_th: 0,
      backup_f: 0,
      backup_s: 0,
      backup_su: 0,
      bktypes: ["Full", "Incremental"],
      backupType: "",
      roadauthoritys: ["KeNHA", "KURA"],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: true,
      name: "",
      inputbkname: true,
      roadauthority: "",
      prefix: "",
      myindex: "",
      mybindex: "",
      editmode: false,
      modaltitle: "Add",
      restorepass: "",
      orderData: [],
      bData: [],
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "backup_name",
          label: "File Name",
          sortable: true,
        },
        {
          key: "backup_time",
          label: "Backup Time",
          sortable: true,
        },
        {
          key: "backup_path",
          label: "Folder Path",
          sortable: true,
        },
        {
          key: "backupDays",
          label: "Backup Days",
          sortable: true,
        },
        "action",
      ],
      updateBDta: false,
      progress: 1,
      options: {
        text: {
          color: "#FFFFFF",
          shadowEnable: true,
          shadowColor: "#000000",
          fontSize: 14,
          fontFamily: "Helvetica",
          dynamicPosition: false,
          hideText: false,
        },
        progress: {
          color: "#2dbd2d",
          backgroundColor: "#333333",
          inverted: false,
        },
        layout: {
          height: 100,
          width: 140,
          verticalTextAlign: 61,
          horizontalTextAlign: 50,
          zeroOffset: 30,
          strokeWidth: 10,
          progressPadding: 4,
          type: "circle",
        },
      },
      bfields: [
        {
          key: "id",
          label: "# ▼",
          sortable: true,
        },
        {
          key: "backupName",
          label: "Backup Name",
          sortable: true,
        },
        {
          key: "bkPath",
          label: "Backup Path",
          sortable: true,
        },
        "action",
      ],
    };
  },
  computed: {
    /**
     * Total no. of records
     */
    rows() {
      return this.orderData.length;
    },
  },
  watch: {
    updateBDta() {
      this.updatebData();
    },
  },
  created() {},
  mounted() {
    // Set the initial number of items
    this.updatearray();
    this.updatebData();
    this.focusInput();
    this.totalRows = this.items.length;
    this.progress;
  },
  methods: {
    //backup & Restore
    createbackup() {
      var start = new Date();
      for (var i = 0; i < this.backupName.length; i++) {
        if (
          this.backupName[i] == "$" ||
          this.backupName[i] == "!" ||
          this.backupName[i] == "/" ||
          this.backupName[i] == "\\" ||
          this.backupName[i] == "@" ||
          this.backupName[i] == "#" ||
          this.backupName[i] == "%"
        ) {
          this.backupName.replace(this.backupName[i], "_").trim();
        }
      }
      //alert(this.backupName);
      Swal.fire({
        title: "Please Wait..",
        html: "Creating backup!", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      axios
        .post(
          window.$http +
            "DbBackup/CreateBackup?folderpath=" +
            this.filefolderpath.trim() +
            "&backupName=" +
            this.backupName,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          console.log(response);
          Swal.close();
          this.updateBDta = true;
          Swal.fire({
            position: "center",
            icon: "success",
            title:
              "Backup completed in " +
              (new Date().getHours() - start.getHours()) +
              "hr(s) " +
              (new Date().getMinutes() - start.getMinutes()) +
              "min(s) " +
              (new Date().getSeconds() - start.getSeconds()) +
              "s " +
              (new Date().getMilliseconds() - start.getMilliseconds()) +
              "ms",
            showConfirmButton: false,
            timer: 3000,
          }).then((e) => {
            Swal.close(e);
          });
        });
    },
    addRec() {
      console.log(this.backupDay);
      console.log(this.backupTime);
      if (this.backupDay.includes("Monday")) {
        this.backup_m = 1;
      } else {
        this.backup_m = 0;
      }
      if (this.backupDay.includes("Tuesday")) {
        this.backup_t = 1;
      } else {
        this.backup_t = 0;
      }
      if (this.backupDay.includes("Wednesday")) {
        this.backup_w = 1;
      } else {
        this.backup_w = 0;
      }
      if (this.backupDay.includes("Thursday")) {
        this.backup_th = 1;
      } else {
        this.backup_th = 0;
      }
      if (this.backupDay.includes("Friday")) {
        this.backup_f = 1;
      } else {
        this.backup_f = 0;
      }
      if (this.backupDay.includes("Saturday")) {
        this.backup_s = 1;
      } else {
        this.backup_s = 0;
      }
      if (this.backupDay.includes("Sunday")) {
        this.backup_su = 1;
      } else {
        this.backup_su = 0;
      }
      var data = {
        backup_name: this.bkname,
        backup_time: this.backupTime,
        backup_path: this.bkPath,
        backup_m: this.backup_m,
        backup_t: this.backup_t,
        backup_w: this.backup_w,
        backup_th: this.backup_th,
        backup_f: this.backup_f,
        backup_s: this.backup_s,
        backup_su: this.backup_su,
        fullincr: "F",
        delete_status: 0,
      };
      console.log(data);
      axios
        .post(window.$http + "BackUpDB", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
          this.updatearray();
          Swal.fire({
            title: "Success!",
            html: "Your work has been saved!",
            icon: "success",
            showCancelButton: true,
          });
        })
        .catch((e) => {
          console.log(e);
        });
    },
    checkpass(backupName) {
      this.backupName = backupName;
    },
    restorebackup() {
      if (this.restorepass != "Netflix201501$!!") {
        Swal.fire({
          title: "Incorrect password!",
          html: "Please enter correct password to continue!",
          allowOutsideClick: false,
          icon: "danger",
          showConfirmButton: true,
        });
        return;
      }
      var start = new Date();
      Swal.fire({
        title:
          "Are you sure you want to restore system back to " +
          this.backupName +
          "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#34c38f",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, restore system!",
      }).then((result) => {
        if (result.value) {
          Swal.fire({
            title: "Please Wait..",
            html: "System restore in progress!", // add html attribute if you want or remove
            allowOutsideClick: false,
            showConfirmButton: false,
            willOpen: () => {
              Swal.showLoading();
            },
          });
          axios
            .post(
              window.$http +
                "DbBackup/restoreBackup?backupName=" +
                this.backupName,
              {
                headers: window.$headers,
              }
            )
            .then(() => {
              Swal.close();
              Swal.fire(
                "System Restore success!",
                "Process completed in " +
                  (new Date().getHours() - start.getHours()) +
                  ":" +
                  (new Date().getMinutes() - start.getMinutes()) +
                  ":" +
                  (new Date().getSeconds() - start.getSeconds()) +
                  ":" +
                  (new Date().getMilliseconds() - start.getMilliseconds()),
                "success"
              );
            });
        }
      });
    },
    download(folderpath, backupName) {
      this.showbar = true;
      axios
        .get(
          window.$http +
            "DbBackup/downloadBackup?filename=" +
            backupName +
            "&filePath=" +
            folderpath,
          {
            headers: window.$headers,
            responseType: "arraybuffer",
          }
        )
        .then((response) => {
          console.log(response.data);
          var filleurl = window.URL.createObjectURL(
            new Blob([response.data], { type: "application/zip" })
          );
          this.progress = 100;
          this.message = "Complete!";
          var fileLink = document.createElement("a");
          fileLink.href = filleurl;
          fileLink.setAttribute("download", backupName);
          document.body.appendChild(fileLink);
          fileLink.click();
        });
      this.progress = 50;
      this.message = "Downloading...";
    },
    updatebData() {
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
        .get(window.$http + "DbBackup/backupfiles", {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.bData = response.data;
          console.log(this.bData);
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
    updatearray() {
      //screen and roles combination
      //alert(window.$http);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        timer: 3000,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      axios
        .get(window.$http + `BackUpDB`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data);
          this.orderData = response.data;
          this.backupTime = response.data[0].backup_time;
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
    /**
     * Export to CSV
     */
    getrpt() {
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
        Backup_Name: row.name,
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
    },
    handleSubmit() {
      console.log("Error on submit");
    },
    printpdf(pl) {
      this.pl = pl;
      const data = this.orderData.map((row) => ({
        ID: row.id,
        Backup_Name: row.name,
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
    edit(
      index,
      myid,
      backup_name,
      backup_time,
      backup_path,
      m,
      t,
      w,
      th,
      f,
      s,
      su
    ) {
      //alert(rolename);
      this.editmode = true;
      this.modaltitle = "Edit";
      this.id = myid;
      this.bkname = backup_name;
      this.backupTime = backup_time;
      this.bkPath = backup_path;
      this.myindex = index;
      this.backup_m = m;
      this.backup_t = t;
      this.backup_w = w;
      this.backup_th = th;
      this.backup_f = f;
      this.backup_s = s;
      this.backup_su = su;
      this.focusInput();

      //this.$refs.search.focus();
      //$("#search").focus();
    },
    editrec() {
      //alert(this.myindex);
      this.orderData[this.myindex].id = this.id;
      this.orderData[this.myindex].backup_name = this.bkname;
      //this.orderData[this.myindex].prefix = this.prefix;
      if (this.backupDay.includes("Monday")) {
        this.backup_m = 1;
      } else {
        this.backup_m = 0;
      }
      if (this.backupDay.includes("Tuesday")) {
        this.backup_t = 1;
      } else {
        this.backup_t = 0;
      }
      if (this.backupDay.includes("Wednesday")) {
        this.backup_w = 1;
      } else {
        this.backup_w = 0;
      }
      if (this.backupDay.includes("Thursday")) {
        this.backup_th = 1;
      } else {
        this.backup_th = 0;
      }
      if (this.backupDay.includes("Friday")) {
        this.backup_f = 1;
      } else {
        this.backup_f = 0;
      }
      if (this.backupDay.includes("Saturday")) {
        this.backup_s = 1;
      } else {
        this.backup_s = 0;
      }
      if (this.backupDay.includes("Sunday")) {
        this.backup_su = 1;
      } else {
        this.backup_su = 0;
      }
      var data = {
        id: this.id,
        backup_name: this.bkname,
        backup_time: this.backupTime,
        backup_path: this.bkPath,
        backup_m: this.backup_m,
        backup_t: this.backup_t,
        backup_w: this.backup_w,
        backup_th: this.backup_th,
        backup_f: this.backup_f,
        backup_s: this.backup_s,
        backup_su: this.backup_su,
        fullincr: "F",
        delete_status: 0,
      };
      axios
        .put(window.$http + "BackUpDB/" + this.id, data, {
          headers: window.$headers,
        })
        .then((res) => {
          console.log(res.data);
          this.updatearray();
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: true,
          });
        })
        .catch((e) => {
          console.log(e);
        });
    },
    deleterec(index, id, bkname) {
      //alert(rolename);
      this.id = id;
      this.bkname = bkname;
      Swal.fire({
        title:
          "Are you sure, you want to delete " +
          this.bkname +
          "#id:" +
          this.id +
          "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      })
        .then((result) => {
          if (result.value) {
            axios.delete(
              window.$http + "BackUpDB/" + this.id,
              { delete_status: 1 },
              { headers: window.$headers }
            );
            this.orderData.splice(index, 1);
            //this.$delete(this.orderData, this.id - 1);
            Swal.fire(
              "Deleted!",
              this.bkname + " has been deleted.",
              "success"
            );
          }
        })
        .catch((e) => {
          console.log(e);
        });
    },
    clearvalues() {
      this.name = "";
      this.editmode = false;
      this.modaltitle = "Add";
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div ref="content">
      <div class="card">
        <div class="card-body">
          <form @submit.prevent="handleSubmit">
            <div class="row justify-content-between mb-5">
              <div class="col-sm-3">
                <button
                  class="btn btn-secondary waves-effect waves-light uil-export"
                  @click="getrpt()"
                >
                  Export to CSV
                </button>
              </div>

              <div class="col-sm-2">
                <button
                  @click="printpdf('p')"
                  v-b-modal.modal-backup
                  class="btn btn-secondary waves-effect waves-light mdi-file-pdf"
                >
                  Backup & Restore
                </button>
              </div>

              <div class="col-sm-3">
                <button
                  @click="printpdf('p')"
                  v-b-modal.modal-Print
                  class="btn btn-secondary waves-effect waves-light mdi-file-pdf"
                >
                  Print PDF
                </button>
              </div>
              <div class="col-sm-3">
                <button
                  v-b-modal.modal-Edit
                  class="btn btn-secondary waves-effect waves-light uil-focus-add"
                  @click="clearvalues()"
                >
                  Add {{ title }}
                </button>
              </div>
            </div>

            <div class="row">
              <div class="col-sm-12">
                <div class="row" id="print">
                  <div class="col-12">
                    <div>
                      <div class="float-end">
                        <form class="d-inline-flex mb-3"></form>
                      </div>
                    </div>
                    <div
                      class="
                        table table-centered
                        datatable
                        dt-responsive
                        nowrap
                        table-card-list
                        dataTable
                        no-footer
                        dtr-inline
                      "
                    >
                      <div class="row">
                        <div class="col-sm-12 col-md-6">
                          <div
                            id="tickets-table_length"
                            class="dataTables_length"
                          >
                            <label
                              class="d-inline-flex align-items-center fw-normal"
                            >
                              Show&nbsp;
                              <b-form-select
                                v-model="perPage"
                                size="sm"
                                :options="pageOptions"
                              ></b-form-select
                              >&nbsp;entries
                            </label>
                          </div>
                        </div>
                        <!-- Search -->
                        <div class="col-sm-12 col-md-6">
                          <div
                            id="tickets-table_filter"
                            class="dataTables_filter text-md-end"
                          >
                            <label
                              class="d-inline-flex align-items-center fw-normal"
                            >
                              Search:
                              <b-form-input
                                v-model="filter"
                                type="search"
                                placeholder="Search..."
                                class="form-control form-control-sm ms-2"
                              ></b-form-input>
                            </label>
                          </div>
                        </div>
                        <!-- End search -->
                      </div>
                      <!-- Table -->

                      <b-table
                        table-class="table table-centered datatable table-card-list"
                        thead-tr-class="bg-transparent"
                        :items="orderData"
                        :fields="fields"
                        responsive="sm"
                        :per-page="perPage"
                        :current-page="currentPage"
                        :sort-by.sync="sortBy"
                        :sort-desc.sync="sortDesc"
                        :filter="filter"
                        :filter-included-fields="filterOn"
                        @filtered="onFiltered"
                      >
                        <template v-slot:cell(backupDays)="data">
                          <a
                            href="javascript: void(0);"
                            class="text-dark fw-bold"
                            ><span v-if="data.item.backup_m == 1"
                              >{{ "m" }},</span
                            ><span v-if="data.item.backup_t == 1"
                              >{{ "t" }},</span
                            ><span v-if="data.item.backup_w == 1"
                              >{{ "w" }},</span
                            ><span v-if="data.item.backup_th == 1"
                              >{{ "th" }},</span
                            ><span v-if="data.item.backup_f == 1"
                              >{{ "f" }},</span
                            ><span v-if="data.item.backup_s == 1"
                              >{{ "s" }},</span
                            ><span v-if="data.item.backup_su == 1">{{
                              "su"
                            }}</span>
                          </a>
                        </template>
                        <template v-slot:cell(action)="data">
                          <ul class="list-inline mb-0">
                            <li class="list-inline-item">
                              <a
                                href="javascript:void(0);"
                                class="px-2 text-primary"
                                v-b-tooltip.hover
                                title="Edit"
                                v-b-modal.modal-Edit
                                @click="
                                  edit(
                                    data.index,
                                    data.item.id,
                                    data.item.backup_name,
                                    data.item.backup_time,
                                    data.item.backup_path,
                                    data.item.backup_m,
                                    data.item.backup_t,
                                    data.item.backup_w,
                                    data.item.backup_th,
                                    data.item.backup_f,
                                    data.item.backup_s,
                                    data.item.backup_su
                                  )
                                "
                              >
                                <i class="uil uil-pen font-size-18"></i>
                              </a>
                            </li>
                            <li class="list-inline-item">
                              <a
                                href="javascript:void(0);"
                                class="px-2 text-danger"
                                v-b-tooltip.hover
                                title="Delete"
                                @click="
                                  deleterec(
                                    data.index,
                                    data.item.id,
                                    data.item.backup_name
                                  )
                                "
                              >
                                <i class="uil uil-trash-alt font-size-18"></i>
                              </a>
                            </li>
                          </ul>
                        </template>
                      </b-table>
                    </div>
                    <div class="row">
                      <div class="col">
                        <div
                          class="
                            dataTables_paginate
                            paging_simple_numbers
                            float-end
                          "
                        >
                          <ul class="pagination pagination-rounded">
                            <!-- pagination -->
                            <b-pagination
                              v-model="currentPage"
                              :total-rows="rows"
                              :per-page="perPage"
                            ></b-pagination>
                          </ul>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
    <b-modal
      id="modal-backup"
      title="Backup & Restore"
      title-class="m-auto"
      modal-class="overflow-auto"
      hide-footer
      size="xl"
      centered
    >
      <div class="row overflow-auto">
        <div class="col-md-12 overflow-auto">
          <div class="card">
            <div class="card-body">
              <div class="row justify-content-between">
                <div class="col-sm-6">
                  <b-form-group
                    class="mb-3"
                    label="Backup Pat"
                    label-for="bkname-input"
                  >
                    <input
                      class="form-control"
                      type="text"
                      :placeholder="filefolderpath"
                      v-model="filefolderpath"
                    />
                  </b-form-group>
                </div>
                <div class="col-sm-6">
                  <b-form-group
                    class="mb-3"
                    label="Backup Name"
                    label-for="bkname-input"
                  >
                    <input
                      class="form-control"
                      type="text"
                      :placeholder="backupName"
                      v-model="backupName"
                    />
                  </b-form-group>
                </div>
                <div class="col-sm-3">
                  <button
                    class="form-control btn btn-secondary waves-effect waves-light uil-focus-add"
                    @click="createbackup()"
                  >
                    Backup Database
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="row overflow-auto">
        <div class="col-sm-12">
          <div class="row" id="print">
            <div class="col-12">
              <div>
                <div class="float-end">
                  <form class="d-inline-flex mb-3"></form>
                </div>
              </div>
              <div
                class="
                        table table-centered
                        datatable
                        dt-responsive
                        nowrap
                        table-card-list
                        dataTable
                        no-footer
                        dtr-inline
                      "
              >
                <div class="row overflow-auto">
                  <div class="col-sm-12 col-md-6">
                    <div id="tickets-table_length" class="dataTables_length">
                      <label class="d-inline-flex align-items-center fw-normal">
                        Show&nbsp;
                        <b-form-select
                          v-model="perPage"
                          size="sm"
                          :options="pageOptions"
                        ></b-form-select
                        >&nbsp;entries
                      </label>
                    </div>
                  </div>
                  <!-- Search -->
                  <div class="col-sm-12 col-md-6">
                    <div
                      id="tickets-table_filter"
                      class="dataTables_filter text-md-end"
                    >
                      <label class="d-inline-flex align-items-center fw-normal">
                        Search:
                        <b-form-input
                          v-model="filter"
                          type="search"
                          placeholder="Search..."
                          class="form-control form-control-sm ms-2"
                        ></b-form-input>
                      </label>
                    </div>
                  </div>
                  <!-- End search -->
                </div>
                <!-- Table -->

                <b-table
                  table-class="table table-centered datatable table-card-list overflow-auto"
                  thead-tr-class="bg-transparent"
                  :items="bData"
                  :fields="bfields"
                  responsive="sm"
                  :per-page="perPage"
                  :current-page="currentPage"
                  :sort-by.sync="sortBy"
                  :sort-desc.sync="sortDesc"
                  :filter="filter"
                  :filter-included-fields="filterOn"
                  @filtered="onFiltered"
                >
                  <template v-slot:cell(check)="data">
                    <div class="">
                      <input
                        type="button"
                        class="custom-control-input"
                        :id="`contacusercheck${data.item.id}`"
                        value="Submit"
                      />
                      <label
                        class="custom-control-label"
                        :for="`contacusercheck${data.item.id}`"
                      ></label>
                    </div>
                  </template>
                  <template v-slot:cell(id)="data">
                    <a href="javascript: void(0);" class="text-dark fw-bold">{{
                      data.item.id
                    }}</a>
                  </template>
                  <template v-slot:cell(action)="data">
                    <ul class="list-inline mb-0">
                      <li class="list-inline-item">
                        <button
                          @click="
                            download(data.item.bkPath, data.item.backupName)
                          "
                          v-b-modal.modal-download
                          class="px-2 btn-secondary"
                          v-b-tooltip.hover
                          title="Download"
                        >
                          <i
                            class="uil uil-cloud-download font-size-18 text-white"
                          ></i>
                        </button>
                      </li>
                      <li class="list-inline-item">
                        <button
                          class="px-2 btn-secondary"
                          v-b-tooltip.hover
                          title="Restore"
                          v-b-modal.modal-checkpass
                          @click="checkpass(data.item.backupName)"
                        >
                          <i
                            class="uil uil-cloud-upload font-size-18 text-white"
                          ></i>
                        </button>
                      </li>
                    </ul>
                  </template>
                </b-table>
              </div>
              <div class="row">
                <div class="col">
                  <div
                    class="
                            dataTables_paginate
                            paging_simple_numbers
                            float-end
                          "
                  >
                    <ul class="pagination pagination-rounded">
                      <!-- pagination -->
                      <b-pagination
                        v-model="currentPage"
                        :total-rows="rows"
                        :per-page="perPage"
                      ></b-pagination>
                    </ul>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </b-modal>
    <b-modal
      id="modal-checkpass"
      :title="modaltitle"
      hide-footer
      size="lg"
      centered
    >
      <div class="row">
        <div class="col-md-12">
          <div class="card">
            <div class="card-body">
              <form @submit.prevent="restorebackup()">
                <div class="row">
                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-4">
                        Restore Password:
                      </div>
                      <input
                        class="form-control"
                        type="password"
                        placeholder="Entere restore password"
                        v-model="restorepass"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-sm-8 mb-2"></div>
                  <div class="col-sm-4 mb-2">
                    <b-button variant="danger" type="submit">Restore</b-button>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
      <!----@click="position(), $bvModal.hide('modal-1') --->
    </b-modal>
    <b-modal id="modal-Edit" :title="modaltitle" hide-footer size="xl" centered>
      <div class="col-xl-8">
        <div class="card mb-0">
          <b-tabs content-class="p-4" justified class="nav-tabs-custom">
            <b-tab active>
              <form @submit.prevent="handleSubmit">
                <div class="row">
                  <div class="col-sm-6">
                    <b-form-group
                      class="mb-3"
                      label="Backup Name"
                      label-for="bkname-input"
                    >
                      <input
                        class="form-control"
                        type="text"
                        :placeholder="bkname"
                        v-model="bkname"
                      />
                    </b-form-group>
                  </div>
                  <div class="col-sm-6">
                    <b-form-group
                      class="mb-3"
                      label="Backup Path"
                      label-for="bkPath-input"
                    >
                      <input
                        class="form-control"
                        type="text"
                        placeholder="E:\Kenloadimg\kenload"
                        v-model="bkPath"
                      />
                    </b-form-group>
                  </div>
                  <div class="col-sm-6">
                    <b-form-group
                      class="mb-3"
                      label="Backup Time"
                      label-for="backupTime-input"
                    >
                      <b-form-timepicker
                        v-model="backupTime"
                        locale="en"
                      ></b-form-timepicker>
                    </b-form-group>
                  </div>
                  <div class="col-sm-6">
                    <b-form-group
                      class="mb-3"
                      label="Backup Days"
                      label-for="backupDay-input"
                    >
                      <multiselect
                        v-model="backupDay"
                        :options="Days"
                        placeholder="Monday"
                        :multiple="true"
                        :editable="true"
                      ></multiselect>
                    </b-form-group>
                  </div>
                </div>
              </form>
            </b-tab>
          </b-tabs>
          <!-- Nav tabs -->
          <!-- Tab content -->
        </div>
      </div>
      <div class="row">
        <div class="col-sm-0 mb-2 mt-2"></div>
        <div class="col-sm-0 mb-2">
          <b-button v-show="!editmode" variant="dark" @click="addRec()"
            >Add {{ title }}</b-button
          >
          <b-button v-show="editmode" variant="dark" @click="editrec()"
            >Edit {{ title }}</b-button
          >
        </div>
      </div>
    </b-modal>
    <b-modal
      id="modal-download"
      title="Download Progress"
      hide-footer
      size="sm"
      centered
    >
      <div class="ml-8 dt-center">
        <p class="text-success" v-if="progress < 100">File downloading...</p>
        <p class="text-success" v-else>Download complete!</p>
        <progress-bar :options="options" :value="progress" class="mb-4 ml-8" />
      </div>
    </b-modal>
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
