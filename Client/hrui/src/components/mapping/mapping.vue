<script>
import vue2Dropzone from "vue2-dropzone";
import Swal from "sweetalert2";
//import VueGoogleAutocomplete from "vue-google-autocomplete";
import axios from "axios";
import Multiselect from "vue-multiselect";

const newheaders = window.$headers;
newheaders["Content-Type"] = "multipart/form-data";
export default {
  name: "mapping",
  props: {
    title: String,
    records: Array,
    pl: String,
    headers: Object,
    uniqueCars: Object,
  },
  components: {
    vueDropzone: vue2Dropzone,
    Multiselect,
  },
  data() {
    return {
      files: new FormData(),
      file: "",
      concodance: 0,
      dropzoneOptions: {
        url: "https://httpbin.org/post",
        thumbnailWidth: 150,
        maxFilesize: 1024,
        includeStyling: true,
        addRemoveLinks: true,
        headers: newheaders,
      },
      tab1: true,
      tab2: false,
      tab3: false,
      county: "All",
      counties: ["All", "Kisumu County"],
      cats: ["All", "TX_CURR"],
      category: "All",
      data_to_use: "Select a data source",
      data_options: ["api data", "csv data"],
      report: "Select report",
      report_options: ["Comparison File", "Mappings File"],
      show_upload_file: false,
      togglequaters: false,
      selected_report: false,
      qt1: "",
      qt2: "",
      qt3: "",
      qt4: "",
      from: new Date().getFullYear() + "-09-01",
      to: new Date().getFullYear() + "-10-01",
      //quater 1
      qt1from: Number(new Date().getFullYear()) - 1 + "-10-01",
      //qt1from: "2020-10-01",
      qt1to: new Date().getFullYear() + "-12-31",
      //quater 2
      qt2from: new Date().getFullYear() + "-01-01",
      qt2to: new Date().getFullYear() + "-03-31",
      //quater 3
      qt3from: new Date().getFullYear() + "-04-01",
      qt3to: new Date().getFullYear() + "-06-31",
      //quater 4
      qt4from: new Date().getFullYear() + "-07-01",
      qt4to: new Date().getFullYear() + "-09-30",
      comparisondata: [],
      mappeddata: [],
    };
  },
  mounted() {
    this.updatefilters();
  },
  methods: {
    updatefilters() {
      axios
        .get(window.$http + "listscategories", { headers: window.$headers })
        .then((res) => {
          res.data.forEach((item) => {
            this.cats.push(item.category_name);
          });
          axios
            .get(window.$http + "listscounties", { headers: window.$headers })
            .then((res) => {
              res.data.forEach((item) => {
                this.counties.push(item.county_name);
              });
            });
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "warning",
            title: "Error!",
            html: "" + e,
            showConfirmButton: true,
            timer: 3000,
          });
        });
    },
    getdataStatus() {
      this.show_upload_file = false;
      this.togglequaters = false;
      this.selected_report = false;
      if (this.data_to_use == "csv data") {
        this.show_upload_file = true;
      } else if (
        this.data_to_use == "api data" &&
        this.report != "Mapped File" &&
        this.report != "Comparison File"
      ) {
        this.tab2 = true;
      }
      if (this.category == "TX_CURR") {
        this.togglequaters = true;
      } else {
        this.togglequaters = false;
      }
      if (this.report == "Comparison File") {
        this.selected_report = true;
      } else {
        this.selected_report = false;
      }
    },
    triggerMapping() {
      axios
        .get(window.$http + "map_data/" + this.category + "/" + this.mydatenew(this.fromdate) + "/" + this.mydatenew(this.todate) + "/100/", { headers: window.$headers })
        .then((res) => {
          res.data.forEach((item) => {
            this.cats.push(item.category_name);
          });
        }).catch((e) => {
          Swal.fire({
            position: "center",
            icon: "warning",
            title: "Error!",
            html: "" + e,
            showConfirmButton: true,
            timer: 3000,
          });
        });
    },
    handleFileUpload() {
      this.file = this.$refs.file.files[0];
      //this.file = event.target.files[0];
    },
    vfileAdded() {
      this.file = this.$refs.myVueDropzone.getAcceptedFiles()[0];
      console.log(this.$refs.myVueDropzone.getAcceptedFiles()[0]); //your origin image data url
    },
    submitFile() {
      let formData = new FormData();
      formData.append("mapping_files", this.file);
      axios
        .post(window.$http + "listfiles/", formData, {
          headers: newheaders,
        })
        .then((response) => {
          console.log(response.data);
          Swal.fire({
            position: "center",
            icon: "success",
            title: "File submitted successfully!",
            html: "Please click on the Mapping tab to initiate data mapping",
            showConfirmButton: false,
            timer: 3000,
          });
          this.tab2 = true;
        })
        .catch((e) => {
          console.log(e);
          Swal.fire({
            position: "center",
            icon: "warning",
            title: "Error!",
            html: "" + e,
            showConfirmButton: true,
            timer: 3000,
          });
        });
    },
    ///date functions
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
    ////end date functions
    triggerComparison() {
      Swal.fire({
        position: "center",
        icon: "info",
        title: "Please wait...",
        html: "Data comparison mapping in progress..",
        showConfirmButton: false,
        allowOutsideClick: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      //generate_comparison_file/<str:use_api_data>/<str:county>/<str:category>/<date:from_date>/<date:to_date>/<int:limit>/
      var startdate = new Date();
      var enddate = new Date();
      if (this.category === "TX_CURR") {
        if (this.qt1 == true) {
          startdate = this.qt1from;
          enddate = this.qt1to;
        } else if (this.qt2 == true) {
          startdate = this.qt2from;
          enddate = this.qt2to;
        } else if (this.qt3 == true) {
          startdate = this.qt3from;
          enddate = this.qt3to;
        } else if (this.qt4 == true) {
          startdate = this.qt4from;
          enddate = this.qt4to;
        }
      } else {
        startdate = this.mydatenew(new Date(this.from));
        enddate = this.mydatenew(new Date(this.to));
      }
      axios
        .get(
          window.$http +
          "generate_comparison_file/" +
          this.data_to_use +
          "/" +
          this.county +
          "/" +
          this.category +
          "/" +
          startdate +
          "/" +
          enddate +
          "/",
          {
            headers: newheaders,
          }
        )
        .then((response) => {
          console.log(response.data);
          this.tab2 = false;
          this.tab1 = false;
          if (response.data.message.includes("Could not find data")) {
            Swal.fire({
              position: "center",
              icon: "warning",
              title: "Empty dataset!",
              html:
                response.data.message +
                "\nPlease select correct filters and try again.",
              showConfirmButton: true,
              timer: 5000,
            });
            this.tab3 = false;
          } else if (
            response.data.message.includes("Could not find datim file")
          ) {
            Swal.fire({
              position: "center",
              icon: "warning",
              title: "File Not Found!",
              html: response.data.message,
              showConfirmButton: true,
              timer: 5000,
            });
            this.tab1 = true;
          } else {
            Swal.fire({
              position: "center",
              icon: "success",
              title: "Success!",
              html: response.data.message,
              showConfirmButton: true,
              timer: 5000,
            });
            this.tab3 = true;
          }
        })
        .catch((e) => {
          console.log(e);
          Swal.fire({
            position: "center",
            icon: "warning",
            title: "Error!",
            html: "" + e,
            showConfirmButton: true,
            timer: 3000,
          });
        });
    },
    getMappegData() {
      Swal.fire({
        position: "center",
        icon: "info",
        title: "Please wait...",
        html: "Pulling data...",
        showConfirmButton: false,
        allowOutsideClick: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      if (this.report == "Comparison File") {
        var startdate = new Date();
        var enddate = new Date();
        if (this.category === "TX_CURR") {
          if (this.qt1 == true) {
            startdate = this.qt1from;
            enddate = this.qt1to;
          } else if (this.qt2 == true) {
            startdate = this.qt2from;
            enddate = this.qt2to;
          } else if (this.qt3 == true) {
            startdate = this.qt3from;
            enddate = this.qt3to;
          } else if (this.qt4 == true) {
            startdate = this.qt4from;
            enddate = this.qt4to;
          }
        } else {
          startdate = this.mydatenew(new Date(this.from));
          enddate = this.mydatenew(new Date(this.to));
        }
        axios
          .get(
            window.$http +
            "get_comparison_data/" +
            this.county +
            "/" +
            this.category +
            "/" +
            startdate +
            "/" +
            enddate +
            "/",
            {
              headers: newheaders,
            }
          )
          .then((response) => {
            //console.log(response.data);
            this.comparisondata = response.data;
            this.printMappedPDF("l");
            Swal.close();
          })
          .catch((e) => {
            console.log(e);
            Swal.fire({
              position: "center",
              icon: "warning",
              title: "Error!",
              html: "" + e,
              showConfirmButton: true,
              timer: 3000,
            });
          });
      } else if (this.report == "Mapped File") {
        axios
          .get(window.$http + "get_mapped_data", {
            headers: newheaders,
          })
          .then((response) => {
            //console.log(response.data);
            this.mappeddata = response.data;
            Swal.close();
          })
          .catch((e) => {
            console.log(e);
            Swal.fire({
              position: "center",
              icon: "warning",
              title: "Error!",
              html: "" + e,
              showConfirmButton: true,
              timer: 3000,
            });
          });
      }
    },
    printMappedPDF(pl) {
      this.pl = pl;
      var data = [];
      this.concodance = 0;
      if (this.report == "Comparison File") {
        this.comparisondata.forEach((val) => {
          this.concodance += Number(val["concodance"]);
        });
        data = this.comparisondata.map((row) => ({
          facility: row.facility,
          ward: row.ward,
          subcounty: row.subcounty,
          county: row.county,
          MOH_UID: row.MOH_FacilityID,
          DATIM_ID: row.DATIM_Disag_ID,
          MOH_IndicatorID: row.MOH_IndicatorCode,
          indicators: row.indicators,
          DATIM_Name: row.DATIM_Disag_Name,
          khis_data: row.khis_data,
          datim_data: row.datim_data,
          weight: row.weight,
          "concodance(%)": row.concodance,
          "khis-datim": row.khis_minus_datim,
        }));
      } else {
        data = this.mappeddata.map((row) => ({
          Category: row.DATIM_Indicator_Category,
          DATIM_Indicator_ID: row.DATIM_Indicator_ID,
          DATIM_Disag_Name: row.DATIM_Disag_Name,
          DATIM_Disag_ID: row.DATIM_Disag_ID,
          Operation: row.Operation,
          MOH_Indicator_ID: row.MOH_Indicator_ID,
          MOH_Indicator_Name: row.MOH_Indicator_Name,
          Disag_Type: row.Disaggregation_Type,
        }));
      }
      //get headers
      this.title = this.report;
      const headers = Object.keys(data[0]);
      const cars = [];
      Object.entries(data).forEach((val) => {
        const [key] = val;
        console.log(key, val);
        cars.push(Object.values(data[key]));
      });

      const uniqueCars = Array.from(new Set(cars));
      this.headers = headers;
      this.uniqueCars = uniqueCars;
      this.records = data;
      this.title = this.report;
      this.$emit("myrecords", {
        records: this.records,
        uniqueCars: this.uniqueCars,
        headers: this.headers,
        title: this.county + " " + this.category + " " + this.title,
        pl: this.pl,
        concodance: this.concodance,
      });
    },
  },
};
</script>

<template>
  <div class="bg-secondary">
    <!-- <PageHeader :title="title" :items="items" /> -->
    <div class="row mb-4 bg-secondary">
      <div class="col-xl-4 m-auto">
        <div class="card h-100 m-auto">
          <div class="card-body bg-secondary">
            <div class="text-center">
              <div>
                <img src="@/assets/images/datamapping.png" alt class="img-fluid m-auto" />
              </div>
            </div>

            <div class="text-muted"></div>
          </div>
        </div>
      </div>
      <div class="col-xl-8">
        <div class="card mb-0">
          <b-tabs content-class="p-4" justified class="nav-tabs-custom">
            <b-tab :active="tab1">
              <template v-slot:title class="">
                <i class="uil uil-upload-alt font-size-20"></i>
                <span class="d-none d-sm-block">Upload Files</span>
              </template>
              <div class="row d-flex flex-row">
                <div class="col-lg-6 col-md-3">
                  <h3 class="my-3">Select data to use</h3>
                  <div class="row">
                    <div class="col-sm-12 col-md-12">
                      <b-form-group label="Data Source" label-for="data-input">
                        <multiselect class="form-control" v-model="data_to_use" :options="data_options"
                          :placeholder="data_to_use" :multiple="false" :editable="true" @input="getdataStatus()">
                        </multiselect>
                      </b-form-group>
                    </div>
                  </div>
                </div>
              </div>
              <div v-show="show_upload_file">
                <div class="row d-flex flex-row">
                  <div class="col-lg-6 col-md-3">
                    <div class="my-3">
                      Upload Data Mapping Files, ONE by ONE
                    </div>
                    <vue-dropzone id="dropzone" ref="myVueDropzone" :use-custom-slot="true" :options="dropzoneOptions"
                      @vdropzone-file-added="vfileAdded" @vdropzone-success="vfileAdded">
                      <div class="dropzone-custom-content">
                        <i class="display-4 text-muted bx bxs-cloud-upload"></i>
                        <h4>Drop files here or click to upload.</h4>
                      </div>
                    </vue-dropzone>
                  </div>
                  <div class="col-lg-6 m-auto">
                    <div class="mt-lg-5">
                      <b-button type="submit" variant="dark" class="uil uil-upload-alt m-4" @click="submitFile()">Finish
                        Upload</b-button>
                    </div>
                  </div>
                </div>
              </div>
            </b-tab>
            <b-tab :active="tab2">
              <template v-slot:title class="">
                <i class="uil uil-data-sharing font-size-20"></i>
                <span class="d-none d-sm-block">Data Transformation</span>
              </template>
              <div class="row">
                <div class="col-lg-12">
                  <div class="border-top">
                    <div class="row">
                      <div class="col-md-12">
                        <div class="card">
                          <div class="card-body">
                            <div class="row">
                              <div class="input-group-text col-sm-12 m-auto">
                                <div class="col-xl-12">
                                  <div class="card mb-0">
                                    <b-tabs content-class="p-4" justified class="nav-tabs-custom">
                                      <b-tab active title="Indicator Mapping" class="text-dark">
                                        <form @submit.prevent="handleSubmit">
                                          <div class="row">
                                            <div class="col-sm-6 col-md-6">
                                              <b-form-group label="County" label-for="county-input">
                                                <multiselect class="form-control" v-model="county" :options="counties"
                                                  placeholder="All" :multiple="false" :editable="true"></multiselect>
                                              </b-form-group>
                                            </div>
                                            <div class="col-sm-6 col-md-6">
                                              <b-form-group label="Category" label-for="Category-input">
                                                <multiselect class="form-control" v-model="category" :options="cats"
                                                  placeholder="All" :multiple="false" :editable="true"></multiselect>
                                              </b-form-group>
                                            </div>
                                          </div>
                                          <div class="row">
                                            <div class="col-sm-6 mb-2">
                                              <div class="border-top">
                                                <div class="mt-lg-5">
                                                  <b-button type="submit" variant="dark" class="uil uil-database-alt"
                                                    @click="triggerMapping()">Trigger Indicator
                                                    Mapping</b-button>
                                                </div>
                                              </div>
                                            </div>
                                          </div>
                                        </form>
                                      </b-tab>
                                      <b-tab title="Indicator Comparison" class="text-dark">
                                        <form @submit.prevent="handleSubmit">
                                          <div class="row">
                                            <div class="col-sm-6 col-md-6">
                                              <b-form-group label="County" label-for="county-input">
                                                <multiselect class="form-control" v-model="county" :options="counties"
                                                  placeholder="Kisumu County" :multiple="false" :editable="true">
                                                </multiselect>
                                              </b-form-group>
                                            </div>
                                            <div class="col-sm-6 col-md-6">
                                              <b-form-group label="Category" label-for="Category-input">
                                                <multiselect class="form-control" v-model="category" :options="cats"
                                                  placeholder="TX_CURR" :multiple="false" :editable="true"
                                                  @input="getdataStatus()"></multiselect>
                                              </b-form-group>
                                            </div>
                                            <div class="col-sm-6 col-md-6 mt-2" v-show="!togglequaters">
                                              <div id="tickets-table-date-picker">
                                                <label>
                                                  From&nbsp;
                                                  <date-picker class="form-control" v-model="from"
                                                    placeholder="2022-09-27" type="date"></date-picker>
                                                </label>
                                              </div>
                                            </div>
                                            <div class="col-sm-6 col-md-6 mt-2" v-show="!togglequaters">
                                              <div id="tickets-table-date-picker">
                                                <label>
                                                  To&nbsp;
                                                  <date-picker class="form-control" v-model="to"
                                                    placeholder="2022-09-27" type="date"></date-picker>
                                                </label>
                                              </div>
                                            </div>
                                            <div class="col-sm-6 col-md-6 mt-2" v-show="togglequaters">
                                              <div id="tickets-table-date-picker">
                                                <label class="d-inline-flex m-2">
                                                  Quater 1(Oct to Dec) &nbsp;
                                                  <b-form-checkbox class="mr-n2" v-model="qt1">
                                                  </b-form-checkbox>
                                                </label>
                                              </div>
                                              <div id="tickets-table-date-picker" v-show="togglequaters">
                                                <label class="d-inline-flex m-2">
                                                  Quater 2(Jan to Mar)&nbsp;
                                                  <b-form-checkbox class="mr-n2" v-model="qt2">
                                                  </b-form-checkbox>
                                                </label>
                                              </div>
                                            </div>
                                            <div class="col-sm-6 col-md-6 mt-2" v-show="togglequaters">
                                              <div id="tickets-table-date-picker">
                                                <label class="d-inline-flex m-2">
                                                  Quater 3(Apr to Jun) &nbsp;
                                                  <b-form-checkbox class="mr-n2" v-model="qt3">
                                                  </b-form-checkbox>
                                                </label>
                                              </div>
                                              <div id="tickets-table-date-picker" class="d-block"
                                                v-show="togglequaters">
                                                <label class="d-inline-flex m-2">
                                                  Quater 4(Jul to Sept) &nbsp;
                                                  <b-form-checkbox class="mr-n2" v-model="qt4">{{ qt4 }}
                                                  </b-form-checkbox>
                                                </label>
                                              </div>
                                            </div>
                                          </div>

                                          <div class="row">
                                            <div class="col-sm-6 mb-2">
                                              <div class="border-top">
                                                <div class="mt-lg-5">
                                                  <b-button type="submit" variant="dark" class="uil uil-database-alt"
                                                    @click="triggerComparison()">Trigger Indicator
                                                    Comparison</b-button>
                                                </div>
                                              </div>
                                            </div>
                                          </div>
                                        </form>
                                      </b-tab>
                                    </b-tabs>
                                  </div>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </b-tab>
            <b-tab :active="tab3">
              <template v-slot:title class="">
                <i class="uil uil-download-alt font-size-20"></i>
                <span class="d-none d-sm-block">Download Files</span>
              </template>
              <div class="row">
                <div class="col-lg-12">
                  <div class="border-top">
                    <div class="row">
                      <div class="col-md-12">
                        <div class="card">
                          <div class="card-body">
                            <div class="row d-flex flex-row">
                              <div class="col-lg-12 col-md-6">
                                <h3 class="my-3">Generate Report</h3>
                                <div class="row">
                                  <div class="col-sm-12 col-md-12">
                                    <b-form-group label="Select report" label-for="report-input">
                                      <multiselect class="form-control" v-model="report" :options="report_options"
                                        :placeholder="report" :multiple="false" :editable="true"
                                        @input="getdataStatus()"></multiselect>
                                    </b-form-group>
                                  </div>
                                  <div class="col-sm-6 col-md-6">
                                    <b-form-group label="County" label-for="county-input">
                                      <multiselect class="form-control" v-model="county" :options="counties"
                                        placeholder="Kisumu County" :multiple="false" :editable="true"></multiselect>
                                    </b-form-group>
                                  </div>
                                  <div class="col-sm-6 col-md-6">
                                    <b-form-group label="Category" label-for="Category-input">
                                      <multiselect class="form-control" v-model="category" :options="cats"
                                        placeholder="TX_CURR" :multiple="false" :editable="true"
                                        @input="getdataStatus()"></multiselect>
                                    </b-form-group>
                                  </div>
                                  <div class="col-sm-6 col-md-6 mt-2" v-show="!togglequaters">
                                    <div id="tickets-table-date-picker">
                                      <label>
                                        From&nbsp;
                                        <date-picker class="form-control" v-model="from" placeholder="2022-09-27"
                                          type="date"></date-picker>
                                      </label>
                                    </div>
                                  </div>
                                  <div class="col-sm-6 col-md-6 mt-2" v-show="!togglequaters">
                                    <div id="tickets-table-date-picker">
                                      <label>
                                        To&nbsp;
                                        <date-picker class="form-control" v-model="to" placeholder="2022-09-27"
                                          type="date"></date-picker>
                                      </label>
                                    </div>
                                  </div>
                                  <div class="col-sm-6 col-md-6 mt-2" v-show="togglequaters">
                                    <div id="tickets-table-date-picker">
                                      <label class="d-inline-flex m-2">
                                        Quater 1(Oct to Dec) &nbsp;
                                        <b-form-checkbox class="mr-n2" v-model="qt1">
                                        </b-form-checkbox>
                                      </label>
                                    </div>
                                    <div id="tickets-table-date-picker" v-show="togglequaters">
                                      <label class="d-inline-flex m-2">
                                        Quater 2(Jan to Mar) &nbsp;
                                        <b-form-checkbox class="mr-n2" v-model="qt2">
                                        </b-form-checkbox>
                                      </label>
                                    </div>
                                  </div>
                                  <div class="col-sm-6 col-md-6 mt-2" v-show="togglequaters">
                                    <div id="tickets-table-date-picker">
                                      <label class="d-inline-flex m-2">
                                        Quater 3(Apr to Jun) &nbsp;
                                        <b-form-checkbox class="mr-n2" v-model="qt3">
                                        </b-form-checkbox>
                                      </label>
                                    </div>
                                    <div id="tickets-table-date-picker" class="d-block" v-show="togglequaters">
                                      <label class="d-inline-flex m-2">
                                        Quater 4(Jul to Sept) &nbsp;
                                        <b-form-checkbox class="mr-n2" v-model="qt4">
                                        </b-form-checkbox>
                                      </label>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>
                        <!----@click="position(), $bvModal.hide('modal-1') --->
                        <div class="row">
                          <div class="col-sm-10 text-right">{{ report }}</div>

                          <div class="col-sm-2">
                            <b-button pill variant="outline-primary" @click="
  [
    getMappegData(),
    printMappedPDF('l'),
    $bvModal.hide('modal-1'),
  ]
                            " v-b-modal.modal-Print style="margin-right: 10px">
                              Generate</b-button>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </b-tab>
          </b-tabs>
          <!-- Nav tabs -->
          <!-- Tab content -->
        </div>
      </div>
    </div>
    <!-- end row -->
  </div>
</template>
