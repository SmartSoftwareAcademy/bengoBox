<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import addusers from "@/components/widgets/security/addusers";
import reportdet from "@/components/report/header";
import Swal from "sweetalert2";
import axios from "axios";

export default {
  name: "userslist",
  page: {
    title: "List Users",
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
    addusers,
    reportdet,
  },
  data() {
    return {
      email: "",
      names: "",
      organization: "",
      roles: [],
      cell: "",
      sendemail: false,
      sendsms: false,
      headers: null,
      uniqueCars: null,
      showme: true,
      pl: "",
      editmode: false,
      title: "Users List",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Users List",
          active: true,
        },
      ],
      orderData: [],
      myid: "test id",
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [1, 10, 25, 50, 100],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: false,
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "email",
          label: "Email",
          sortable: true,
        },
        {
          key: "fullnames",
          label: "Full Names",
          sortable: true,
        },
        {
          key: "organization",
          label: "Organization",
          sortable: true,
        },

        {
          key: "userrole[0].userrole_name",
          label: "Role(s)",
          sortable: true,
        },
        {
          key: "phone",
          label: "Cell Phone",
          sortable: true,
        },
        {
          key: "sendmail",
          label: "Send Mail",
          sortable: true,
        },
        {
          key: "sendsms",
          label: "Send SMS",
          sortable: true,
        },
        "action",
      ],
    };
  },
  created() {
    this.fetchusers();
  },
  methods: {
    fetchusers() {
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
        .get(window.$http + `Users`, { headers: window.$headers })
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
        });
    },
    printpdf(pl) {
      //console.log(this.tokenString);
      this.pl = pl;
      const data = this.orderData.map((row) => ({
        ID: row.id,
        Email: row.email,
        Full_Name: row.names,
        Organization: row.organization,
        Role: row.roles,
        cell: row.cell,
        Send_Email: row.sendmail,
        Send_SMS: row.sendsms,
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
        Cargo_Name: row.name,
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
    deleterec(index, myid, email, names) {
      this.myid = myid;
      this.email = email;
      this.names = names;
      Swal.fire({
        title:
          "Are you sure, you want to delete? " +
          this.email +
          " for " +
          this.names,
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#34c38f",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          this.orderData.splice(index, 1);
          Swal.fire(
            this.email + " Deleted!",
            "Your record has been deleted.",
            "success"
          );
        }
      });
    },
    edit(
      index,
      id,
      email,
      names,
      organization,
      roles,
      cell,
      sendemail,
      sendsms
    ) {
      this.editmode = true;
      //alert(name);
      this.myid = index;
      this.id = id;
      this.email = email;
      this.names = names;
      this.organization = organization;
      this.roles = roles;
      this.cell = cell;
      this.sendemail = sendemail;
      this.sendsms = sendsms;

      alert("sendemail" + this.sendemail + this.myid);
    },
    clearvalues() {
      this.email = "";
      this.names = "";
      this.organization = "";
      this.roles = [];
      this.sendmail = "";
      this.sendsms = "";

      this.editmode = false;
      this.modaltitle = "Add Role";
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="card">
      <div class="card-body">
        <form @submit.prevent="handleSubmit">
          <div class="card">
            <div class="card-body">
              <div class="row justify-content-between">
                <div class="col-sm-6">
                  <button
                    class="btn btn-primary waves-effect waves-light uil-export"
                    @click="getrpt()"
                  >
                    Export to CSV
                  </button>
                </div>

                <div class="col-sm-2">
                  <button
                    @click="printpdf('p')"
                    v-b-modal.modal-Print
                    class="
                      btn btn-primary
                      waves-effect waves-light
                      mdi-file-pdf
                    "
                  >
                    Print PDF
                  </button>
                </div>
                <div class="col-sm-2">
                  <button
                    v-b-modal.modal-Add
                    class="
                      btn btn-primary
                      waves-effect waves-light
                      uil-focus-add
                    "
                    @click="clearvalues()"
                  >
                    Add User
                  </button>
                </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-sm-12">
              <div class="card">
                <div class="card-body changebg">
                  <div class="row">
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
                                class="
                                  d-inline-flex
                                  align-items-center
                                  fw-normal
                                "
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
                                class="
                                  d-inline-flex
                                  align-items-center
                                  fw-normal
                                "
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
                          <template v-slot:cell(roles)="data">
                            <a
                              href="javascript: void(0);"
                              class="text-dark fw-bold"
                              v-for="screen in data.item.roles"
                              :key="screen"
                              >{{ screen }} <br
                            /></a>
                          </template>
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
                                <a
                                  href="javascript:void(0);"
                                  class="px-2 text-primary"
                                  v-b-tooltip.hover
                                  title="Edit"
                                  v-b-modal.modal-Add
                                  @click="
                                    edit(
                                      data.index,
                                      data.item.id,
                                      data.item.email,
                                      data.item.names,
                                      data.item.organization,
                                      data.item.roles,
                                      data.item.cell,
                                      data.item.sendmail,
                                      data.item.sendsms
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
                                      data.item.email,
                                      data.item.names
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
            </div>
          </div>
        </form>
      </div>
    </div>

    <b-modal id="modal-1" title="Delete Record" hide-footer size="sm" centered>
      <div>
        Are you sure you want to delete
        <a class="text-dark fw-bold">{{ email }}</a>

        for record Number {{ myid }}
      </div>
      <div>Description{{ names }}</div>
      <div class="row">
        <div class="col-sm-6">
          <button class="btn btn-primary waves-effect waves-light">
            Delete
          </button>
        </div>
        <div class="col-sm-6">
          <b-button class="" block @click="$bvModal.hide('modal-1')"
            >Cancel</b-button
          >
        </div>
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
    <b-modal id="modal-Add" title="Add Users" hide-footer size="xl" centered>
      <addusers
        :email="email"
        :names="names"
        :organization="organization"
        :roles="roles"
        :cell="cell"
        :sendemail="sendemail"
        :sendsms="sendsms"
        :description="description"
        :editmode="editmode"
        :orderData="orderData"
        :selectedlists="selectedlists"
      >
      </addusers>
    </b-modal>
  </Layout>
</template>

<style>
.changebg {
  background-color: #f7f6ebfb;
}
</style>