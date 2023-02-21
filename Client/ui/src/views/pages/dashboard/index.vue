<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import Stat from "@/components/widgets/stat";
//import SalesAnalytics from "./sales-analytics";
//import SellingProduct from "./selling-product";
import TopUsers from "./top-users";
//import Activity from "./activity";
//import SocialSource from "./social-source";

export default {
  page: {
    title: "Dashboard",
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
    Stat,
    // SalesAnalytics,
    //SellingProduct,
    TopUsers,
    //Activity,
    //SocialSource,
  },
  data() {
    return {
      title: "Dashboard",
      items: [
        {
          text: "BengoBox",
        },
        {
          text: "Dashboard",
          active: true,
        },
      ],
    };
  },
  mounted() {
    //this.getIP();
  },
  methods: {

  }
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <!-- <Stat /> -->
    <div class="row">
      <!-- <SalesAnalytics /> -->
      <div class="col-xl-3">
        <div class="row">
          <div class="card" style="background:#ffcccb">
            <div class="card-body">
              <div class="row align-items-center">
                <div class="col-sm-8">
                  <p class="text-white font-size-18">
                    NSSF
                    <b>RATES</b>
                    <i class="mdi mdi-arrow-right"></i>
                  </p>
                </div>
                <div class="col-sm-6 bg-gradient">
                  <div class="mt-4 mt-sm-0">
                    <P class="text-danger">Your payroll application is using the <b>new NSSF rates</b></P>
                    <p class="badge badge-pill bg-success">Use Old Rates?</p>
                  </div>
                </div>
              </div>
            </div>
            <!-- end card-body-->
          </div>
          <TopUsers />
        </div>
        <!-- end card-->
        <!-- <SellingProduct /> -->

      </div>
      <div class="col-xl-6">
        <div>
          <b-card-group deck>
            <b-card header="PAYROLL HISTORY" header-tag="header" footer="" footer-tag="footer" title="">
              <b-card-text>Process your monthly payroll with ease.</b-card-text>
              <p href="#" variant="primary">Payroll last generated <span
                  class="badge badge-pill bg-success">01/01/2023</span></p>
              <template #footer>
                <b-button class="btn-sm btn-success uil-sync p-1"> Process Payroll</b-button>
              </template>
            </b-card>

            <b-card title="" header-tag="header" footer-tag="footer">
              <template #header>
                <h6 class="mb-0">REPORTING</h6>
              </template>
              <div class="row">
                <div class="col-sm-12">
                  <div class="row">
                    <div class="col-sm-3">
                      <div class="card text-center">
                        <img src="@/assets/images/kra.png" class="img-thumbnail fluid rounded-circle" alt="" />
                        <a href="#">KRA Reports</a>
                      </div>
                    </div>
                    <div class="col-sm-3">
                      <div class="card text-center">
                        <img src="@/assets/images/nhif.png" class="img-thumbnail fluid rounded-circle" alt="" />
                        <a href="#">NHIF Reports</a>
                      </div>
                    </div>
                    <div class="col-sm-3">
                      <div class="card text-center">
                        <img src="@/assets/images/nssf.png" class="img-thumbnail fluid rounded-circle" alt="" />
                        <a href="#">NSSF Reports</a>
                      </div>
                    </div>
                    <div class="col-sm-3">
                      <div class="card text-center">
                        <img src="@/assets/images/nita.png" class="img-thumbnail fluid rounded-circle" alt="" />
                        <a href="#">NITA Reports</a>
                      </div>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col-sm-3">
                      <div class="card text-center">
                        <i class="fa fa-chart-bar fa-4x" alt=""></i>
                        <a href="#">Analytical Reports</a>
                      </div>
                    </div>
                    <div class="col-sm-3">
                      <div class="card text-center">
                        <i class="fa fa-calendar fa-4x" alt=""></i>
                        <a href="#">Monthly Reports</a>
                      </div>
                    </div>
                    <div class="col-sm-3">
                      <div class="card text-center">
                        <i class="fa fa-dollar-sign fa-4x" alt=""></i>
                        <a href="#">Net Pay Reports</a>
                      </div>
                    </div>
                    <div class="col-sm-3">
                      <div class="card text-center">
                        <i class="fa fa-file fa-4x" alt=""></i>
                        <a href="#">Custom Reports</a>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-sm-6 col-md-6"></div>
              </div>
            </b-card>
          </b-card-group>
        </div>
      </div>
      <div class="col-xl-3">
        <div>
          <b-card-group deck>
            <b-card header="MANAGE EMPLOYEES" header-tag="header" footer="" footer-tag="footer" title="">
              <b-card-text>Manage employee database with ease.</b-card-text>
              <p href="#" variant="primary">Active employee accounts <span class="badge badge-pill bg-success">0</span>
              </p>
              <template #footer>
                <div>
                  <b-dropdown id="dropdown-buttons" text="Action" class="m-2 uil-arrow-down bg-success">
                    <b-dropdown-item-button class="">Add Employees</b-dropdown-item-button>
                    <b-dropdown-item-button active class="">Import Employes</b-dropdown-item-button>
                    <b-dropdown-item-button class="">Employee List</b-dropdown-item-button>
                  </b-dropdown>
                </div>
              </template>
            </b-card>

            <b-card title="" header-tag="header" footer-tag="footer">
              <template #header>
                <h6 class="mb-0">USER ACCOUNTS</h6>
              </template>
              <stat />
            </b-card>
            <b-card title="" header-tag="header" footer-tag="footer">
              <template #header>
                <h6 class="mb-0">USER ACCOUNTS</h6>
              </template>
              <p>Email 2022 P9 Forms to all your employees
                <br />It's that time of the year again. Individual Returns!
              </p>
              <template #footer>
                <div>
                  <b-button class="uil-message bg-success">Email P9 Forms</b-button>&nbsp;
                  <b-button class="uil-print bg-success">Print P9 Forms</b-button>
                </div>
              </template>
            </b-card>
          </b-card-group>
        </div>
      </div>
    </div>
    <div class="row">
      <!-- <Activity /> -->
      <!-- <SocialSource /> -->
    </div>
  </Layout>
</template>
