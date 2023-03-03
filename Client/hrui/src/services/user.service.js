import { authHeader } from "./auth-header";
import store from "@/state/store";
var CryptoJS = require("crypto-js");
import axios from "axios";
import Swal from "sweetalert2";

export const userService = {
  login,
  logout,
  register,
  getAll,
  data() {
    return {
      user_email: "",
    };
  },
};

function login(email, password) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password }),
  };
  store.dispatch("screens/clearScreens");
  var sanitizedScreens = [];
  return fetch(window.$http + "AuthManagement/Login", requestOptions)
    .then(handleResponse)
    .then((user) => {
      axios
        .get(window.$http + `UserRoleScreens/byMail/` + email, {
          headers: { Authorization: `Bearer ${user.token}` },
        })
        .then((response) => {
          let screensData = response.data;
          console.log(screensData);
          let userScreens = screensData.screens.split(",");
          //remove duplicate screens
          for (var j = 0; j < userScreens.length; j++) {
            sanitizedScreens.push(userScreens[j]);
          }
          store.dispatch("screens/loadUserScreens", {
            userScreen: sanitizedScreens,
          });
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Could not load your Access Screens Contact Admin" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });
        });
      let accessScreens = [
        "dashboard",
        //payroll
        "PayRoll",//main
        "AdvancePay",
        "LossesDamage",
        "ExpenseClaims",
        "RegularEmployees",
        //sumenu
        "ViewPayslips",
        "Processpayroll",
        //
        "CasualEmployees",
        "Consultants",
        "EmailSlips",
        "ScheduledSlips",
        "PrintSlips",
        //leave
        "Leave",//main
        "Applicatios",
        "ApplyLeave",
        "Entitlement",
        "Logs",
        "OpeningBalances",
        "Categories",
        //attendance
        "Attendance",//main
        "ClockInDevices",
        "EnrollEmployees",
        "AttendanceLogs",
        //submenu
        "DailyLogs",
        "MonthyLogs",
        "Overview",
        "Overtime",
        //
        "OverTime",
        //submenu
        "View",
        "Add",
        "Bulk Overtime",
        //
        "Absenteesm",
        //submenu
        "View",
        "Add",
        "Bulk Add",
        //
        //Employees
        "Employees",//main
        "View",
        "ManageContacts",
        "Add",
        //submenu
        "AddEmployee",
        "ImportEmployee",
        //
        "PayRollData",
        //submenu
        "BasicPay",
        "Benefits",
        "Earnings",
        "Deductions",
        "Loans",
        //
        "EmployeeData",
        //submenu
        "PersonalData",
        "HRData",
        "PayData",
        //
        "WorkShifts",
        "TerminateEmployment",
        //Reports
        "Reports",//main
        "PayRollReports",
        //submenu
        "MasterRoll",
        "PayRollSummary",
        "CustomPayReports",
        "PayRollGeneralLedger",
        "PayRollReconciliations",
        "PayRollTurnOver",
        "LiabilityReports",
        //
        "PayMentReports",//main
        //submenu
        "NetPayReports",
        "AdvancePayments",
        "ExpenseClaimPayments",
        //
        "StatutoryReports",
        //submenu
        "KRAReports",
        "WithHoldingTax",
        "NHIFReports",
        "NSSFReports",
        "NITAReports",
        //
        "AttendanceReports",
        "LeaveReport",
        "AuditLogs",
        //settings
        "Settings",//main
        "roles",
        "passwordPolicy",
        "Users",
        "changePassword",
        "auditLogs",
        "backupDatabase",
        "systemSettings",
        "HRMSettings",
        //submenu
        "JobTitles",
        "Departments",
        "Regions",
        "Projects",
        "WorkersUnions",
        "WorkShiftSettings",
        "Holidays",
        "ESSSettings",
        //
        "PayRollSettings",
        //submenu
        "Approvals",
        "Deductions",
        "Loans",
        "Benefits",
        "Earnings",
        "DefaultSettings",
        "Formulas",
        "CustomizePayslip",
        "Banks",
        //
        "ExpenseClaimSettings",
        "GeneralHR",
        "MyCompanies",
        "CurrencyTime",
        "LookFeel",
        "Taxes",
        "APISettings",
        "BengoBox",
        "SMS",

      ];

      for (var i = 0; i < accessScreens.length; i++) {
        store.dispatch("screens/loadUserScreens", { userScreen: accessScreens[i] });
      }
      let encryptToken = CryptoJS.AES.encrypt(
        user.token.trim(),
        "mnopqr",
      ).toString();
      if (user.token) {
        let responseJson = {
          id: user.id,
          username: email,
          name: user.fullname,
          email: email,
          token: encryptToken,
        };
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem("user", JSON.stringify(responseJson));
      }
      return user;
    })
    .then((error) => {
      return error;
    });
}

function logout() {
  // remove user from local storage to log user out
  // var currentdate = new Date();
  // const data = {
  //   datetime: currentdate,
  //   useremail: this.user_email,
  //   application: window.navigator.appName,
  //   details: `Logged Out`,
  //   computer: localStorage.clientip,
  // };
  // axios
  //   .post(window.$http + "AuditLogs", data, { headers: window.$headers })
  //   .then((response) => {
  //     // var data = {
  //     //   status: false,
  //     //   active_hours: 0,
  //     //   time_out_hours: 0,
  //     //   shift_date: new Date(),
  //     //   user_id: Number(localStorage.user_id),
  //     // }
  //     // axios.post(window.$http + `Shifts/ActiveHours`, data, { headers: window.$headers })
  //     //   .then((res) => {
  //     //     console.log(res.data);
  //     //   })
  //     console.log(response.data);
  //   })
  //   .catch((e) => {
  //     console.log(e);
  //   });
  localStorage.removeItem("user");
  //localStorage.removeItem("user_id");
  //localStorage.removeItem("passwordalert");
}

function register(user) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(user),
  };
  return fetch(`/AuthManagement/Register`, requestOptions).then(handleResponse);
}

function getAll() {
  const requestOptions = {
    method: "GET",
    headers: authHeader(),
  };
  return fetch(`/users`, requestOptions).then(handleResponse);
}

function handleResponse(response) {
  return response.text().then((text) => {
    var data = text && JSON.parse(text);
    if (!response.ok) {
      //var error = (data && data.message) || response.statusText;
      if (response.status === 401) {
        // auto logout if 401 response returned from api
        logout();
        location.reload(true);
      }
      return Promise.reject(data);
    }
    return data;
  });
}