<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Swal from "sweetalert2";
import axios from "axios";
import DatePicker from "vue2-datepicker";
import Multiselect from "vue-multiselect";

export default {
  page: {
    title: "Audit Logs",
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
    DatePicker,
    Multiselect,
  },
  data() {
    return {
      title: "Audit Logs",
      from_date: "12/27/2021",
      to_date: "12/28/2021",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Audit Logs",
          active: true,
        },
      ],

      orderData: [
        {
          id: "#1",
          datetime: "2021-07-18T17:23:16",
          useremail: "admin@masterspace.com",
          screen: "Prosecution",
          desc: "Opened Prosecution list",
          print: "1",
          comp: "192.168.0.10",
        },
      ],
      log_id: "",
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      userList: [],
      users: [],
      user_email: "",
      role_allowed: false,
      logged_user_role: "",
      pageOptions: [1, 10, 25, 50, 100, 500],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: true,
      from: new Date(),
      to: new Date(),
      fields: [
        {
          key: "id",
          label: "id",
          sortable: true,
        },
        {
          key: "datetime",
          label: "Date",
          sortable: true,
        },
        {
          key: "useremail",
          label: "User",
          sortable: true,
        },
        {
          key: "application",
          label: "Application",
          sortable: true,
        },
        {
          key: "details",
          label: "Details",
          sortable: true,
        },
        {
          key: "computer",
          label: "Computer",
          sortable: true,
        },
        "Action",
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
  mounted() {
    this.from_date = new Date();
    this.to_date = new Date();
    // Set the initial number of items
    this.getrec();
    this.upadtearray();
    this.totalRows = this.items.length;
  },
  methods: {
    upadtearray() {
      axios
        .get(window.$http + `Users`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.userList = response.data;
          this.logged_user_role = this.userList.filter((e) => e.email === JSON.parse(localStorage.user).email)[0].userRole.toString().toLowerCase();
          if (
            this.logged_user_role.toLowerCase().includes('superadmin') ||
            this.logged_user_role.toLowerCase().includes('superuser') ||
            this.logged_user_role.toLowerCase().includes('developer') ||
            this.logged_user_role.toLowerCase().includes('clustermanager') ||
            this.logged_user_role.toLowerCase().includes('cm')
          ) {
            this.role_allowed = true;
          }
          this.userList.forEach((usr) => {
            this.users.push(usr.email);
          });
        });
    },
    filterLogs() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      var todate = this.mydatenew(new Date(this.to)) + "T23:59:00.000Z";
      var fromdate = this.mydatenew(new Date(this.from)) + "T23:59:00.000Z";
      axios
        .get(
          window.$http +
          "AuditLogs/search?fromdate=" +
          fromdate +
          "&todate=" +
          todate +
          "&email=" + this.user_email.toString() +
          "&limit=" + this.perPage,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          //console.log(this.orderData);
          console.log(response.data);
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
    getrec() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      //alert();
      axios
        .get(window.$http + `AuditLogs?limit=` + this.perPage, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.

          this.orderData = response.data;

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
     * Search the table data with search input
     */
    getmydate(mydate) {
      let d = new Date(mydate);
      let year = d.getFullYear();
      let month = this.getmonth(d.getMonth());
      let date = d.getDate();
      date = this.getv(date);
      //month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = date + "/" + month + "/" + year;
      return datetime;
    },
    formatDate(date) {
      var d = new Date(date),
        month = "" + (d.getMonth() + 1),
        day = "" + d.getDate(),
        year = d.getFullYear();

      if (month.length < 2) month = "0" + month;
      if (day.length < 2) day = "0" + day;

      return [year, month, day].join("-");
    },
    mydatenew(d) {
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      //alert("month" + month);
      let date = d.getDate();
      date = this.getv(date);
      month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = year + "-" + month + "-" + date;
      return datetime;
    },
    mydate(d, day, mon, yea) {
      let year = d.getFullYear() - yea;
      let month = d.getMonth() + 1 - mon;
      //alert("month" + month);
      let date = d.getDate() - day;
      date = this.getv(date);
      month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = year + "-" + month + "-" + date;
      return datetime;
    },
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
    },
    getmonth(d) {
      const monthNames = [
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec",
      ];
      return monthNames[d];
    },
    getdatefilter() {
      //alert(this.mydate(new Date(), 0, 0, 0));
      this.todate = new Date(this.mydate(new Date(), 0, 0, 0));
      if (this.picked == "Day") {
        this.fromdate = new Date(this.mydate(new Date(), 1, 0, 0));
      }
      if (this.picked == "week") {
        this.fromdate = new Date(this.mydate(new Date(), 7, 0, 0));
      }
      if (this.picked == "month") {
        this.fromdate = new Date(this.mydate(new Date(), 0, 1, 0));
      }
      if (this.picked == "year") {
        this.fromdate = new Date(this.mydate(new Date(), 0, 0, 1));
      }
    },
    getcurrentdate() {
      let d = new Date();
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      let date = d.getDate();
      date = this.getv(date);
      month = this.getv(month);

      let hour = d.getHours();
      let min = d.getMinutes();
      let sec = d.getSeconds();
      hour = this.getv(hour);
      min = this.getv(min);
      sec = this.getv(sec);

      //const msec = d.getMilliseconds();
      const datetime =
        year + "-" + month + "-" + date + "T" + hour + ":" + min + ":" + sec;
      return datetime;
    },
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    editrec() {
      Swal.fire(
        "Operation not allowed!" +
        "\nLog entry editing is not allowed for any user account"
      );
    },
    //delete logs
    deleterec(index, log_id, useremail) {
      //alert(rolename);
      this.log_id = log_id;
      this.useremail = useremail;
      if (!this.role_allowed) {
        Swal.fire(
          "Operation not allowed!" +
          "\nLog entry delete is not allowed for this user account"
        );
        return;
      }
      Swal.fire({
        title:
          "Are you sure, you want to delete log entry " +
          this.log_id +
          " for " +
          this.useremail +
          "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          this.orderData.splice(index, 1);
          axios
            .delete(window.$http + `AuditLogs/` + this.log_id, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.orderData.splice(index, 1);
              Swal.fire(
                this.useremail + " log entry " + this.log_id + " Deleted!",
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
    handleSubmit() {
      console.log("Error on submit");
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="card">
      <div class="card-body overflow-auto">
        <form @submit.prevent="handleSubmit">
          <div class="row">
            <div class="col-sm-12">
              <div class="row">
                <div class="col-12">
                  <div>
                    <div class="float-end">
                      <form class="d-inline-flex mb-3"></form>
                    </div>
                  </div>
                  <div class="
                      table table-centered
                      datatable
                      dt-responsive
                      nowrap
                      table-card-list
                      dataTable
                      no-footer
                      dtr-inline
                    ">
                    <div class="row">
                      <div class="col-sm-12 col-md-12">
                        <div id="tickets-table_length" class="dataTables_length">
                          <label class="d-inline-flex align-items-center fw-normal">
                            Show&nbsp;
                            <b-form-select v-model="perPage" size="sm" @change="getrec()"
                              :options="pageOptions"></b-form-select>&nbsp;entries
                          </label>
                        </div>
                      </div>
                      <!-- custom filters -->
                      <div class="row">
                        <div class="col-sm-3 col-md-2 m-2">
                          <b-form-group label="User(s)" label-for="role-input">
                            <multiselect class="text-white" v-model="user_email" :options="users"
                              placeholder="Select User" :multiple="true" :editable="true">
                            </multiselect>
                          </b-form-group>
                        </div>
                        <div class="col-sm-4 col-md-2 m-2 mt-3">
                          <div id="tickets-table-date-picker">
                            <label>
                              From&nbsp;
                              <date-picker value="2021/05/17" v-model="from" :first-day-of-week="1" lang="en"
                                placeholder="Pick a Date"></date-picker>
                            </label>
                          </div>
                        </div>
                        <div class="col-sm-3 col-md-2 m-2 mt-3">
                          <div id="tickets-table-date-picker">
                            <label>
                              To&nbsp;
                              <date-picker value="2021/05/17" v-model="to" :first-day-of-week="1" lang="en"
                                placeholder="Pick a Date">
                              </date-picker>
                            </label>
                          </div>
                        </div>
                        <div class="col-sm-3 col-md-2 mt-4 m-auto">
                          <div id="tickets-table-date-picker">
                            <b-button variant="dark" class="uil uil-search" @click="filterLogs()">Search</b-button>
                          </div>
                        </div>
                      </div>
                      <!-- end filters -->
                      <!-- Search -->
                      <div class="col-sm-3 col-md-2">
                        <div id="tickets-table_filter" class="dataTables_filter text-md-end">
                          <label class="d-inline-flex align-items-center fw-normal">
                            Search:
                            <b-form-input v-model="filter" type="search" placeholder="Search..."
                              class="form-control form-control-sm ms-2"></b-form-input>
                          </label>
                        </div>
                      </div>
                      <!-- End search -->
                    </div>
                    <!-- Table -->

                    <b-table table-class="table table-centered datatable table-card-list"
                      thead-tr-class="bg-transparent" :items="orderData" :fields="fields" responsive="sm"
                      :per-page="perPage" :current-page="currentPage" :sort-by.sync="sortBy" :sort-desc.sync="sortDesc"
                      :filter="filter" :filter-included-fields="filterOn" @filtered="onFiltered">
                      <!----
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
                            <a
                              href="javascript: void(0);"
                              class="text-dark fw-bold"
                              >{{ data.item.id }}</a
                            >
                          </template>

                          <template v-slot:cell(name)="data">
                            <a href="#" class="text-body">{{
                              data.item.name
                            }}</a>
                          </template>
                          <template v-slot:cell(status)="data">
                            <div
                              class="badge bg-pill bg-soft-success font-size-12"
                              :class="{
                                'bg-soft-danger':
                                  data.item.status === 'Chargeback',
                                'bg-soft-warning':
                                  data.item.status === 'unpaid',
                              }"
                            >
                              {{ data.item.status }}
                            </div>
                          </template>
                          --->
                      <template v-slot:cell(action)="data">
                        <ul class="list-inline mb-0">
                          <li class="list-inline-item">
                            <a href="javascript:void(0);" class="px-2 text-primary" v-b-tooltip.hover title="Edit"
                              @click="editrec()">
                              <i class="uil uil-pen font-size-18"></i>
                            </a>
                          </li>
                          <li class="list-inline-item">
                            <a href="javascript:void(0);" class="px-2 text-danger" v-b-tooltip.hover title="Delete"
                              @click="
                                deleterec(
                                  data.index,
                                  data.item.id,
                                  data.item.useremail
                                )
                              ">
                              <i class="uil uil-trash-alt font-size-18"></i>
                            </a>
                          </li>
                        </ul>
                      </template>
                    </b-table>
                  </div>
                  <div class="row">
                    <div class="col">
                      <div class="
                          dataTables_paginate
                          paging_simple_numbers
                          float-end
                        ">
                        <ul class="pagination pagination-rounded">
                          <!-- pagination -->
                          <b-pagination v-model="currentPage" :total-rows="rows" :per-page="perPage"></b-pagination>
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
  </Layout>
</template>
<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
</style>
