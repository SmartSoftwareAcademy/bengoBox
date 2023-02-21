export const state = {
    userScreens: ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
    userToken: "token",
    bidirection: "",
    cameratype: "",
    vehicleinyard: "",
};
export const getters = {
    getUserScreens(state) {
        console.log(state.userScreens);
        return state.userScreens;
    },
    getUserToken(state) {
        return state.userToken;
    },
};

export const mutations = {
    LOAD_USER_SCREENS(state, userScreen) {
        //console.log(userScreen);
        const allscreens = new Set();
        let index = 0;
        const createdMenus = new Set();
        for (let i = 0; i < userScreen.length; i++)
            allscreens.add(userScreen[i]);

        //parse array
        //store in hashset check if exist in hashset
        //check if array exists then set
        //
        if (allscreens.has("dashboard")) {
            //alert(allscreens.keys("dashboard"))
            let newObjectScreen = {
                id: 1,
                label: "menuitems.dashboard.text",
                icon: "uil-home-alt",
                link: "/",

            };

            state.userScreens[0] = newObjectScreen;
        }

        ////////////////////////////////////////ProcessPay///////////////////////////////////////
        if (allscreens.has("AdvancePay")) {
            if (createdMenus.has("ProcessPay")) {
                let newObjectScreen = {
                    id: 80,
                    label: "Advance Pay",
                    link: "/ProcessPay/AdvancePay",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Process Pay");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 2,
                    label: "Process Pay",
                    icon: "uil-dollar-sign",
                    link: "/ProcessPay",
                    subItems: [
                        {
                            id: 1,
                            label: "Advance Pay",
                            link: "/ProcessPay/AdvancePay",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("ProcessPay");
            }
        }
        if (allscreens.has("LossesDamages")) {
            if (createdMenus.has("ProcessPay")) {
                let newObjectScreen = {
                    id: 3,
                    label: "Losses & Damages",
                    link: "/ProcessPay/LossesDamages",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        //console.log(e.label)
                        return e.label;
                    })
                    .indexOf("Process Pay");
                //console.log(index);
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 2,
                    label: "Process Pay",
                    icon: "uil-dollar-sign",
                    link: "/ProcessPay",
                    subItems: [
                        {
                            id: 3,
                            label: "Losses & Damages",
                            link: "/ProcessPay/LossesDamages",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("ProcessPay");
            }
        }
        if (allscreens.has("ExpensesClaims")) {
            if (createdMenus.has("ProcessPay")) {
                let newObjectScreen = {
                    id: 4,
                    label: "Expenses & Claims",
                    link: "/ProcessPay/ExpensesClaims",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Process Pay");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 2,
                    label: "ProcessPay",
                    icon: "uil-dollar-sign",
                    link: "/ProcessPay",
                    subItems: [
                        {
                            id: 4,
                            label: "Expenses & Claims",
                            link: "/ProcessPay/ExpensesClaims",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("ProcessPay");
            }
        }
        if (allscreens.has("RegularEmployees")) {
            if (createdMenus.has("ProcessPay")) {
                let newObjectScreen = {
                    id: 5,
                    label: "Regular Employees",
                    link: "/ProcessPay/RegularEmployees",
                    subItems: [
                        {
                            id: 1,
                            label: "View Payslips",
                            link: "/ProcessPay/ViewPayslips",
                        },
                        {
                            id: 2,
                            label: "Processpayroll",
                            link: "/ProcessPay/Processpayroll",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Process Pay");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 2,
                    label: "ProcessPay",
                    icon: "uil-dollar-sign",
                    link: "/ProcessPay",
                    subItems: [
                        {
                            id: 5,
                            label: "Regular Employees",
                            link: "/ProcessPay/RegularEmployees",
                            subItems: [
                                {
                                    id: 1,
                                    label: "View Payslips",
                                    link: "/ProcessPay/ViewPayslips",
                                },
                                {
                                    id: 2,
                                    label: "Processpayroll",
                                    link: "/ProcessPay/Processpayroll",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("ProcessPay");
            }
        }
        if (allscreens.has("CasualEmployees")) {
            if (createdMenus.has("ProcessPay")) {
                let newObjectScreen = {
                    id: 6,
                    label: "Casual Employees",
                    link: "/ProcessPay/CasualEmployees",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Process Pay");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 2,
                    label: "ProcessPay",
                    icon: "uil-dollar-sign",
                    link: "/ProcessPay",
                    subItems: [
                        {
                            id: 6,
                            label: "CasualEmployees",
                            link: "/ProcessPay/CasualEmployees",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("ProcessPay");
            }
        }
        if (allscreens.has("Consultants")) {
            if (createdMenus.has("ProcessPay")) {
                let newObjectScreen = {
                    id: 7,
                    label: "Consultants",
                    link: "/ProcessPay/Consultants",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Process Pay");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 2,
                    label: "ProcessPay",
                    icon: "uil-dollar-sign",
                    link: "/ProcessPay",
                    subItems: [
                        {
                            id: 7,
                            label: "Consultants",
                            link: "/ProcessPay/Consultants",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("ProcessPay");
            }
        }
        if (allscreens.has("EmailSlips")) {
            if (createdMenus.has("ProcessPay")) {
                let newObjectScreen = {
                    id: 8,
                    label: "Email Slips",
                    link: "/ProcessPay/EmailSlips",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Process Pay");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 2,
                    label: "ProcessPay",
                    icon: "uil-dollar-sign",
                    link: "/ProcessPay",
                    subItems: [
                        {
                            id: 1,
                            label: "Email Slips",
                            link: "/ProcessPay/EmailSlips",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("ProcessPay");
            }
        }
        if (allscreens.has("ScheduledSlips")) {
            if (createdMenus.has("ProcessPay")) {
                let newObjectScreen = {
                    id: 9,
                    label: "Scheduled Slips",
                    link: "/ProcessPay/ScheduledSlips",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Process Pay");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 2,
                    label: "ProcessPay",
                    icon: "uil-dollar-sign",
                    link: "/ProcessPay",
                    subItems: [
                        {
                            id: 9,
                            label: "Scheduled Slips",
                            link: "/ProcessPay/ScheduledSlips",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("ProcessPay");
            }
        }
        if (allscreens.has("PrintSlips")) {
            if (createdMenus.has("ProcessPay")) {
                let newObjectScreen = {
                    id: 97,
                    label: "Print Slips",
                    link: "/ProcessPay/PrintSlips",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Process Pay");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 86,
                    label: "ProcessPay",
                    icon: "uil-dollar-sign",
                    link: "/ProcessPay",
                    subItems: [
                        {
                            id: 97,
                            label: "Print Slips",
                            link: "/ProcessPay/PrintSlips",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("ProcessPay");
            }
        }
        ////////////////////////////////////////////////LEAVE///////////////////////////////////////////////////////////////////
        if (allscreens.has("Applications")) {
            if (createdMenus.has("Leave")) {
                let newObjectScreen = {
                    id: 1,
                    label: "Applications",
                    link: "/Leave/Applications",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Leave");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 3,
                    label: "Leave",
                    icon: "uil-plane-departure",
                    link: "/Leave",
                    subItems: [
                        {
                            id: 1,
                            label: "Applications",
                            link: "/Leave/Applications",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Leave");
            }
        }
        if (allscreens.has("ApplyLeave")) {
            if (createdMenus.has("Leave")) {
                let newObjectScreen = {
                    id: 2,
                    label: "ApplyLeave",
                    link: "/Leave/ApplyLeave",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        //console.log(e.label)
                        return e.label;
                    })
                    .indexOf("Leave");
                //console.log(index);
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 3,
                    label: "Leave",
                    icon: "uil-plane-departure",
                    link: "/Leave",
                    subItems: [
                        {
                            id: 2,
                            label: "ApplyLeave",
                            link: "/Leave/ApplyLeave",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Leave");
            }
        }
        if (allscreens.has("Entitlement")) {
            if (createdMenus.has("Leave")) {
                let newObjectScreen = {
                    id: 3,
                    label: "Entitlement",
                    link: "/Leave/Entitlement",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Leave");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 3,
                    label: "Leave",
                    icon: "uil-plane-departure",
                    link: "/Leave",
                    subItems: [
                        {
                            id: 3,
                            label: "Entitlement",
                            link: "/Leave/Entitlement",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Leave");
            }
        }
        if (allscreens.has("Logs")) {
            if (createdMenus.has("Leave")) {
                let newObjectScreen = {
                    id: 4,
                    label: "Logs",
                    link: "/Leave/Logs",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Leave");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 3,
                    label: "Leave",
                    icon: "uil-plane-departure",
                    link: "/Leave",
                    subItems: [
                        {
                            id: 4,
                            label: "Logs",
                            link: "/Leave/Logs",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Leave");
            }
        }
        if (allscreens.has("OpeningBalances")) {
            if (createdMenus.has("Leave")) {
                let newObjectScreen = {
                    id: 5,
                    label: "Opening Balances",
                    link: "/Leave/OpeningBalances",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Leave");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 3,
                    label: "Leave",
                    icon: "uil-plane-departure",
                    link: "/Leave",
                    subItems: [
                        {
                            id: 5,
                            label: "Opening Balances",
                            link: "/Leave/OpeningBalances",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Leave");
            }
        }
        if (allscreens.has("Categories")) {
            if (createdMenus.has("Leave")) {
                let newObjectScreen = {
                    id: 6,
                    label: "Categories",
                    link: "/Leave/Categories",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Leave");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 3,
                    label: "Leave",
                    icon: "uil-plane-departure",
                    link: "/Leave",
                    subItems: [
                        {
                            id: 6,
                            label: "Categories",
                            link: "/Leave/Categories",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Leave");
            }
        }
        ////////////////////////////////////////////////ATTENDANCE///////////////////////////////////////////////////////////////////
        if (allscreens.has("ClockInDevices")) {
            if (createdMenus.has("Attendance")) {
                let newObjectScreen = {
                    id: 1,
                    label: "Clock-In Devices",
                    link: "/Attendance/ClockInDevices",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Attendance");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 4,
                    label: "Attendance",
                    icon: "uil-calendar-alt",
                    link: "/Attendance",
                    subItems: [
                        {
                            id: 1,
                            label: "Clock-In Devices",
                            link: "/Attendance/ClockInDevices",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Attendance");
            }
        }
        if (allscreens.has("EnrollEmployees")) {
            if (createdMenus.has("Attendance")) {
                let newObjectScreen = {
                    id: 1,
                    label: "Enroll Employees",
                    link: "/Attendance/EnrollEmployees",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        //console.log(e.label)
                        return e.label;
                    })
                    .indexOf("Attendance");
                //console.log(index);
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 3,
                    label: "Attendance",
                    icon: "uil-calendar-alt",
                    link: "/Attendance",
                    subItems: [
                        {
                            id: 1,
                            label: "Enroll Employees",
                            link: "/Attendance/EnrollEmployees",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Attendance");
            }
        }
        if (allscreens.has("AttendanceLogs")) {
            if (createdMenus.has("Leave")) {
                let newObjectScreen = {
                    id: 4,
                    label: "Attendance Logs",
                    link: "/Attendance/AttendanceLogs",
                    subItems: [
                        {
                            id: 1,
                            label: "Daily Logs",
                            link: "/Attendance/DailyLogs",
                        },
                        {
                            id: 1,
                            label: "Monthy Logs",
                            link: "/Attendance/MonthyLogs",
                        },
                        {
                            id: 1,
                            label: "Overview",
                            link: "/Attendance/Overview",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Attendance");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 5,
                    label: "Attendance",
                    icon: "uil-calendar-alt",
                    link: "/Attendance",
                    subItems: [
                        {
                            id: 4,
                            label: "Attendance Logs",
                            link: "/Attendance/AttendanceLogs",
                            subItems: [
                                {
                                    id: 1,
                                    label: "Daily Logs",
                                    link: "/Attendance/DailyLogs",
                                },
                                {
                                    id: 1,
                                    label: "Monthy Logs",
                                    link: "/Attendance/MonthyLogs",
                                },
                                {
                                    id: 1,
                                    label: "Overview",
                                    link: "/Attendance/Overview",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Attendance");
            }
        }
        if (allscreens.has("OverTime")) {
            if (createdMenus.has("Attendance")) {
                let newObjectScreen = {
                    id: 5,
                    label: "OverTime",
                    link: "/Attendance/OverTime",
                    subItems: [
                        {
                            id: 1,
                            label: "View",
                            link: "/Attendance/ViewOverTime",
                        },
                        {
                            id: 2,
                            label: "Add",
                            link: "/Attendance/AddOverTime",
                        },
                        {
                            id: 3,
                            label: "Bulk Overtime",
                            link: "/Attendance/BulkOvertime",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Attendance");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 3,
                    label: "Attendance",
                    icon: "uil-calendar-alt",
                    link: "/Attendance",
                    subItems: [
                        {
                            id: 3,
                            label: "OverTime",
                            link: "/Attendance/OverTime",
                            subItems: [
                                {
                                    id: 1,
                                    label: "View",
                                    link: "/Attendance/View",
                                },
                                {
                                    id: 2,
                                    label: "Add",
                                    link: "/Attendance/Add",
                                },
                                {
                                    id: 3,
                                    label: "Bulk Overtime",
                                    link: "/Attendance/BulkOvertime",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Attendance");
            }
        }
        if (allscreens.has("Absenteesm")) {
            if (createdMenus.has("Attendance")) {
                let newObjectScreen = {
                    id: 5,
                    label: "Absenteesm",
                    link: "/Attendance/Absenteesm",
                    subItems: [
                        {
                            id: 1,
                            label: "View",
                            link: "/Attendance/ViewAbsenteesm",
                        },
                        {
                            id: 2,
                            label: "Add",
                            link: "/Attendance/AddAbsenteesm",
                        },
                        {
                            id: 3,
                            label: "Bulk Absenteesm",
                            link: "/Attendance/BulkAbsenteesm",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Attendance");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 3,
                    label: "Attendance",
                    icon: "uil-calendar-alt",
                    link: "/Attendance",
                    subItems: [
                        {
                            id: 3,
                            label: "Absenteesm",
                            link: "/Attendance/Absenteesm",
                            subItems: [
                                {
                                    id: 1,
                                    label: "View",
                                    link: "/Attendance/ViewAbsenteesm",
                                },
                                {
                                    id: 2,
                                    label: "Add",
                                    link: "/Attendance/AddAbsenteesm",
                                },
                                {
                                    id: 3,
                                    label: "Bulk Absenteesm",
                                    link: "/Attendance/BulkAbsenteesm",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Attendance");
            }
        }
        ////////////////////////////////////////////////EMPLOYEES///////////////////////////////////////////////////////////////////
        if (allscreens.has("ViewEmployees")) {
            if (createdMenus.has("Employees")) {
                let newObjectScreen = {
                    id: 1,
                    label: "View Employees",
                    link: "/Employees/ViewEmployees",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Employees");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 6,
                    label: "Employees",
                    icon: "uil-users-alt",
                    link: "/Employees",
                    subItems: [
                        {
                            id: 1,
                            label: "View Employees",
                            link: "/Employees/ViewEmployees",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Employees");
            }
        }
        if (allscreens.has("ManageContacts")) {
            if (createdMenus.has("Employees")) {
                let newObjectScreen = {
                    id: 2,
                    label: "Manage Contacts",
                    link: "/Employees/ManageContacts",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        //console.log(e.label)
                        return e.label;
                    })
                    .indexOf("Employees");
                //console.log(index);
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 6,
                    label: "Employees",
                    icon: "uil-users-alt",
                    link: "/Employees",
                    subItems: [
                        {
                            id: 2,
                            label: "Manage Contacts",
                            link: "/Employees/ManageContacts",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Employees");
            }
        }
        if (allscreens.has("AddEmployess")) {
            if (createdMenus.has("Employees")) {
                let newObjectScreen = {
                    id: 3,
                    label: "Add Employees",
                    link: "/Employees/AddEmployess",
                    subItems: [
                        {
                            id: 1,
                            label: "Add Employees",
                            link: "/Employees/AddEmployess",
                        },
                        {
                            id: 2,
                            label: "Import Employees",
                            link: "/Employees/ImportEmployee",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Employees");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 6,
                    label: "Employees",
                    icon: "uil-users-alt",
                    link: "/Employees",
                    subItems: [
                        {
                            id: 3,
                            label: "Add Employees",
                            link: "/Employees/AddEmployees",
                            subItems: [
                                {
                                    id: 1,
                                    label: "Add Employees",
                                    link: "/Employees/AddEmployess",
                                },
                                {
                                    id: 2,
                                    label: "Import Employees",
                                    link: "/Employees/ImportEmployee",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Employeess");
            }
        }
        if (allscreens.has("PayRollData")) {
            if (createdMenus.has("Employees")) {
                let newObjectScreen = {
                    id: 4,
                    label: "PayRoll Data",
                    link: "/Employees/PayRollData",
                    subItems: [
                        {
                            id: 1,
                            label: "BasicPay",
                            link: "/Employees/BasicPay",
                        },
                        {
                            id: 2,
                            label: "Benefits",
                            link: "/Employees/Benefits",
                        },
                        {
                            id: 3,
                            label: "Earnings",
                            link: "/Employees/Earnings",
                        },
                        {
                            id: 4,
                            label: "Deductions",
                            link: "/Employees/Deductions",
                        },
                        {
                            id: 5,
                            label: "Loans",
                            link: "/Employees/Loans",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Employees");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 6,
                    label: "Employees",
                    icon: "uil-users-alt",
                    link: "/Employees",
                    subItems: [
                        {
                            id: 4,
                            label: "PayRoll Data",
                            link: "/Employees/PayRollData",
                            subItems: [
                                {
                                    id: 1,
                                    label: "BasicPay",
                                    link: "/Employees/BasicPay",
                                },
                                {
                                    id: 2,
                                    label: "Benefits",
                                    link: "/Employees/Benefits",
                                },
                                {
                                    id: 3,
                                    label: "Earnings",
                                    link: "/Employees/Earnings",
                                },
                                {
                                    id: 4,
                                    label: "Deductions",
                                    link: "/Employees/Deductions",
                                },
                                {
                                    id: 5,
                                    label: "Loans",
                                    link: "/Employees/Loans",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Employees");
            }
        }
        if (allscreens.has("EmployeeData")) {
            if (createdMenus.has("Employees")) {
                let newObjectScreen = {
                    id: 5,
                    label: "Employee Data",
                    link: "/Employees/EmployeeData",
                    subItems: [
                        {
                            id: 1,
                            label: "Personal Data",
                            link: "/Employees/PersonalData",
                        },
                        {
                            id: 2,
                            label: "HR Data",
                            link: "/Employees/HRData",
                        },
                        {
                            id: 3,
                            label: "Payment Data",
                            link: "/Employees/PayData",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Employees");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 3,
                    label: "Attendance",
                    icon: "uil-calendar-alt",
                    link: "/Attendance",
                    subItems: [
                        {
                            id: 3,
                            label: "Absenteesm",
                            link: "/Attendance/Absenteesm",
                            subItems: [
                                {
                                    id: 1,
                                    label: "View",
                                    link: "/Attendance/ViewAbsenteesm",
                                },
                                {
                                    id: 2,
                                    label: "Add",
                                    link: "/Attendance/AddAbsenteesm",
                                },
                                {
                                    id: 3,
                                    label: "Bulk Absenteesm",
                                    link: "/Attendance/BulkAbsenteesm",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Attendance");
            }
        }
        if (allscreens.has("WorkShifts")) {
            if (createdMenus.has("Employees")) {
                let newObjectScreen = {
                    id: 6,
                    label: "Work Shifts",
                    link: "/Employees/WorkShifts",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Employees");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 6,
                    label: "Employees",
                    icon: "uil-users-alt",
                    link: "/Employees",
                    subItems: [
                        {
                            id: 6,
                            label: "Work Shifts",
                            link: "/Employees/WorkShifts",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Employees");
            }
        }
        if (allscreens.has("TerminateEmployment")) {
            if (createdMenus.has("Employees")) {
                let newObjectScreen = {
                    id: 7,
                    label: "Terminate Employment",
                    link: "/Employees/TerminateEmployment",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Employees");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 6,
                    label: "Employees",
                    icon: "uil-users-alt",
                    link: "/Employees",
                    subItems: [
                        {
                            id: 7,
                            label: "Terminate Employment",
                            link: "/Employees/TerminateEmployment",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Employees");
            }
        }
        ////////////////////////////////////////////////REPORTS///////////////////////////////////////////////////////////////////
        if (allscreens.has("PayRollReports")) {
            if (createdMenus.has("Reports")) {
                let newObjectScreen = {
                    id: 1,
                    label: "PayRoll Reports",
                    link: "/Reports/PayRollReports",
                    subItems: [
                        {
                            id: 1,
                            label: "Master Roll",
                            link: "/Reports/MasterRoll",
                        },
                        {
                            id: 2,
                            label: "Custom PayReports",
                            link: "/Reports/CustomPayReports",
                        },
                        {
                            id: 3,
                            label: "PayRoll Summary",
                            link: "/Reports/PayRollSummary",
                        },
                        {
                            id: 4,
                            label: "PayRoll General Ledger",
                            link: "/Reports/PayRollGeneralLedger",
                        },
                        {
                            id: 5,
                            label: "PayRoll Reconciliations",
                            link: "/Reports/PayRollReconciliations",
                        },
                        {
                            id: 6,
                            label: "PayRoll TurnOver",
                            link: "/Reports/PayRollTurnOver",
                        },
                        {
                            id: 7,
                            label: "Liability Reports",
                            link: "/Reports/LiabilityReports",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Reports");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 7,
                    label: "Reports",
                    icon: "uil-graph-bar",
                    link: "/Reports",
                    subItems: [
                        {
                            id: 1,
                            llabel: "PayRoll Reports",
                            link: "/Reports/PayRollReports",
                            subItems: [
                                {
                                    id: 1,
                                    label: "Master Roll",
                                    link: "/Reports/MasterRoll",
                                },
                                {
                                    id: 2,
                                    label: "Custom PayReports",
                                    link: "/Reports/CustomPayReports",
                                },
                                {
                                    id: 3,
                                    label: "PayRoll Summary",
                                    link: "/Reports/PayRollSummary",
                                },
                                {
                                    id: 4,
                                    label: "PayRoll General Ledger",
                                    link: "/Reports/PayRollGeneralLedger",
                                },
                                {
                                    id: 5,
                                    label: "PayRoll Reconciliations",
                                    link: "/Reports/PayRollReconciliations",
                                },
                                {
                                    id: 6,
                                    label: "PayRoll TurnOver",
                                    link: "/Reports/PayRollTurnOver",
                                },
                                {
                                    id: 7,
                                    label: "Liability Reports",
                                    link: "/Reports/LiabilityReports",
                                },
                            ]
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Reports");
            }
        }
        if (allscreens.has("PayMentReports")) {
            if (createdMenus.has("Reports")) {
                let newObjectScreen = {
                    id: 2,
                    label: "PayMent Reports",
                    link: "/Reports/PayMentReports",
                    subItems: [
                        {
                            id: 1,
                            label: "NetPay Reports",
                            link: "/Reports/NetPayReports",
                        },
                        {
                            id: 2,
                            label: "Advance Payments",
                            link: "/Reports/AdvancePayments",
                        },
                        {
                            id: 3,
                            label: "Expense Claim Payments",
                            link: "/Reports/ExpenseClaimPayments",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Reports");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 7,
                    label: "Reports",
                    icon: "uil-graph-bar",
                    link: "/Reports",
                    subItems: [
                        {
                            id: 2,
                            label: "PayMent Reports",
                            link: "/Reports/PayMentReports",
                            subItems: [
                                {
                                    id: 1,
                                    label: "NetPay Reports",
                                    link: "/Reports/NetPayReports",
                                },
                                {
                                    id: 2,
                                    label: "Advance Payments",
                                    link: "/Reports/AdvancePayments",
                                },
                                {
                                    id: 3,
                                    label: "Expense Claim Payments",
                                    link: "/Reports/ExpenseClaimPayments",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Reports");
            }
        }
        if (allscreens.has("StatutoryReports")) {
            if (createdMenus.has("Reports")) {
                let newObjectScreen = {
                    id: 3,
                    label: "Statutory Reports",
                    link: "/Reports/StatutoryReports",
                    subItems: [
                        {
                            id: 1,
                            label: "KRA Reports",
                            link: "/Reports/KRAReports",
                        },
                        {
                            id: 2,
                            label: "WithHolding Tax",
                            link: "/Reports/WithHoldingTax",
                        },
                        {
                            id: 3,
                            label: "NHIF Reports",
                            link: "/Reports/NHIFReports",
                        },
                        {
                            id: 4,
                            label: "NSSFReports",
                            link: "/Reports/NSSFReports",
                        },
                        {
                            id: 5,
                            label: "NITA Reports",
                            link: "/Reports/NITAReports",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Reports");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 7,
                    label: "Reports",
                    icon: "uil-graph-bar",
                    link: "/Reports",
                    subItems: [
                        {
                            id: 3,
                            label: "Statutory Reports",
                            link: "/Reports/StatutoryReports",
                            subItems: [
                                {
                                    id: 1,
                                    label: "KRA Reports",
                                    link: "/Reports/KRAReports",
                                },
                                {
                                    id: 2,
                                    label: "WithHolding Tax",
                                    link: "/Reports/WithHoldingTax",
                                },
                                {
                                    id: 3,
                                    label: "NHIF Reports",
                                    link: "/Reports/NHIFReports",
                                },
                                {
                                    id: 4,
                                    label: "NSSFReports",
                                    link: "/Reports/NSSFReports",
                                },
                                {
                                    id: 5,
                                    label: "NITA Reports",
                                    link: "/Reports/NITAReports",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Reports");
            }
        }
        if (allscreens.has("AttendanceReports")) {
            if (createdMenus.has("Reports")) {
                let newObjectScreen = {
                    id: 4,
                    label: "Attendance Reports",
                    link: "/Reports/AttendanceReports",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Reports");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 7,
                    label: "Reports",
                    icon: "uil-graph-bar",
                    link: "/Reports",
                    subItems: [
                        {
                            id: 4,
                            label: "Attendance Reports",
                            link: "/Reports/AttendanceReports",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Reports");
            }
        }
        if (allscreens.has("LeaveReport")) {
            if (createdMenus.has("Reports")) {
                let newObjectScreen = {
                    id: 5,
                    label: "Leave Reports",
                    link: "/Reports/LeaveReport",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Reports");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 7,
                    label: "Reports",
                    icon: "uil-graph-bar",
                    link: "/Reports",
                    subItems: [
                        {
                            id: 5,
                            label: "Leave Reports",
                            link: "/Reports/LeaveReport",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Reports");
            }
        }
        if (allscreens.has("AuditLogs")) {
            if (createdMenus.has("Reports")) {
                let newObjectScreen = {
                    id: 6,
                    label: "AuditLogs",
                    link: "/Reports/AuditLogs",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Reports");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 7,
                    label: "Reports",
                    icon: "uil-graph-bar",
                    link: "/Reports",
                    subItems: [
                        {
                            id: 6,
                            label: "AuditLogs",
                            link: "/Reports/AuditLogs",
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Reports");
            }
        }
        ////////////////////////////////////////////////SETTINGS///////////////////////////////////////////////////////////////////
        if (allscreens.has("HRMSettings")) {
            if (createdMenus.has("Settings")) {
                let newObjectScreen = {
                    id: 1,
                    label: "HRM Settings",
                    link: "/Settings/HRMSettings",
                    subItems: [
                        {
                            id: 1,
                            label: "Job Titles",
                            link: "/Settings/JobTitles",
                        },
                        {
                            id: 2,
                            label: "Departments",
                            link: "/Settings/Departments",
                        },
                        {
                            id: 3,
                            label: "Regions",
                            link: "/Settings/Regions",
                        },
                        {
                            id: 4,
                            label: "Projects",
                            link: "/Settings/Projects",
                        },
                        {
                            id: 5,
                            label: "Workers Unions",
                            link: "/Settings/WorkersUnions",
                        },
                        {
                            id: 6,
                            label: "WorkShift Settings",
                            link: "/Settings/WorkShiftSettings",
                        },
                        {
                            id: 7,
                            label: "Holidays",
                            link: "/Settings/Holidays",
                        },
                        {
                            id: 8,
                            label: "ESS Settings",
                            link: "/Settings/ESSSettings",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Settings");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 8,
                    label: "Settings",
                    icon: "uil-cog",
                    link: "/Settings",
                    subItems: [
                        {
                            id: 1,
                            label: "HRM Settings",
                            link: "/Settings/HRMSettings",
                            subItems: [
                                {
                                    id: 1,
                                    label: "Job Titles",
                                    link: "/Settings/JobTitles",
                                },
                                {
                                    id: 2,
                                    label: "Departments",
                                    link: "/Settings/Departments",
                                },
                                {
                                    id: 3,
                                    label: "Regions",
                                    link: "/Settings/Regions",
                                },
                                {
                                    id: 4,
                                    label: "Projects",
                                    link: "/Settings/Projects",
                                },
                                {
                                    id: 5,
                                    label: "Workers Unions",
                                    link: "/Settings/WorkersUnions",
                                },
                                {
                                    id: 6,
                                    label: "WorkShift Settings",
                                    link: "/Settings/WorkShiftSettings",
                                },
                                {
                                    id: 7,
                                    label: "Holidays",
                                    link: "/Settings/Holidays",
                                },
                                {
                                    id: 8,
                                    label: "ESS Settings",
                                    link: "/Settings/ESSSettings",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Settings");
            }
        }
        if (allscreens.has("PayRollSettings")) {
            if (createdMenus.has("Settings")) {
                let newObjectScreen = {
                    id: 2,
                    label: "PayRoll Settings",
                    link: "/Settings/PayRollSettings",
                    subItems: [
                        {
                            id: 1,
                            label: "Approvals",
                            link: "/Settings/Approvals",
                        },
                        {
                            id: 2,
                            label: "Deductions",
                            link: "/Settings/Deductions",
                        },
                        {
                            id: 3,
                            label: "Loans",
                            link: "/Settings/Loans",
                        },
                        {
                            id: 4,
                            label: "Benefits",
                            link: "/Settings/Benefits",
                        },
                        {
                            id: 5,
                            label: "Earnings",
                            link: "/Settings/Earnings",
                        },
                        {
                            id: 6,
                            label: "Default Settings",
                            link: "/Settings/DefaultSettings",
                        },
                        {
                            id: 7,
                            label: "Formulas",
                            link: "/Settings/Formulas",
                        },
                        {
                            id: 8,
                            label: "Customize Payslip",
                            link: "/Settings/CustomizePayslip",
                        },
                        {
                            id: 9,
                            label: "Banks",
                            link: "/Settings/Banks",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Settings");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 8,
                    label: "Settings",
                    icon: "uil-cog",
                    link: "/Settings",
                    subItems: [
                        {
                            id: 2,
                            label: "PayRoll Settings",
                            link: "/Settings/PayRollSettings",
                            subItems: [
                                {
                                    id: 1,
                                    label: "Approvals",
                                    link: "/Settings/Approvals",
                                },
                                {
                                    id: 2,
                                    label: "Deductions",
                                    link: "/Settings/Deductions",
                                },
                                {
                                    id: 3,
                                    label: "Loans",
                                    link: "/Settings/Loans",
                                },
                                {
                                    id: 4,
                                    label: "Benefits",
                                    link: "/Settings/Benefits",
                                },
                                {
                                    id: 5,
                                    label: "Earnings",
                                    link: "/Settings/Earnings",
                                },
                                {
                                    id: 6,
                                    label: "Default Settings",
                                    link: "/Settings/DefaultSettings",
                                },
                                {
                                    id: 7,
                                    label: "Formulas",
                                    link: "/Settings/Formulas",
                                },
                                {
                                    id: 8,
                                    label: "Customize Payslip",
                                    link: "/Settings/CustomizePayslip",
                                },
                                {
                                    id: 9,
                                    label: "Banks",
                                    link: "/Settings/Banks",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Settings");
            }
        }
        if (allscreens.has("APISettings")) {
            if (createdMenus.has("Settings")) {
                let newObjectScreen = {
                    id: 3,
                    label: "API Settings",
                    link: "/Settings/APISettings",
                    subItems: [
                        {
                            id: 1,
                            label: "BengoBox",
                            link: "/Settings/BengoBox",
                        },
                        {
                            id: 2,
                            label: "SMS",
                            link: "/Settings/SMS",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Settings");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 8,
                    label: "Settings",
                    icon: "uil-cog",
                    link: "/Settings",
                    subItems: [
                        {
                            id: 3,
                            label: "API Settings",
                            link: "/Settings/APISettings",
                            subItems: [
                                {
                                    id: 1,
                                    label: "BengoBox",
                                    link: "/Settings/BengoBox",
                                },
                                {
                                    id: 2,
                                    label: "SMS",
                                    link: "/Settings/SMS",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Settings");
            }
        }
        if (allscreens.has("Security")) {
            if (createdMenus.has("Settings")) {
                let newObjectScreen = {
                    id: 4,
                    label: "Security",
                    link: "/Security",
                    subItems: [
                        {
                            id: 1,
                            label: "Roles",
                            link: "/Security/Roles",
                        },
                        {
                            id: 2,
                            label: "Users",
                            link: "/Security/Users",
                        },
                        {
                            id: 3,
                            label: "Password Policy",
                            link: "/Security/PasswordPolicy",
                        },
                        {
                            id: 4,
                            label: "Backup Database",
                            link: "/Security/BackupDatabase",
                        },
                        {
                            id: 5,
                            label: "System Settings",
                            link: "/Security/systemSettings",
                        },
                    ],
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Settings");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 8,
                    label: "Settings",
                    icon: "uil-cog",
                    link: "/Settings",
                    subItems: [
                        {
                            id: 4,
                            label: "Security",
                            link: "/Settings/Security",
                            subItems: [
                                {
                                    id: 1,
                                    label: "Roles",
                                    link: "/Settings/Roles",
                                },
                                {
                                    id: 2,
                                    label: "Users",
                                    link: "/Settings/Users",
                                },
                                {
                                    id: 3,
                                    label: "Password Policy",
                                    link: "/Settings/PasswordPolicy",
                                },
                                {
                                    id: 4,
                                    label: "Backup Database",
                                    link: "/Settings/backupDatabase",
                                },
                                {
                                    id: 5,
                                    label: "System Settings",
                                    link: "/Settings/systemSettings",
                                },
                            ],
                        },
                    ],
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Settings");
            }
        }
        if (allscreens.has("ExpenseClaimSettings")) {
            if (createdMenus.has("Settings")) {
                let newObjectScreen = {
                    id: 5,
                    label: "ExpenseClaimSettings",
                    link: "/Settings/ExpenseClaimSettings",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Settings");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 8,
                    label: "Settings",
                    icon: "uil-cog",
                    link: "/Settings",
                    subItems: [
                        {
                            id: 5,
                            label: "ExpenseClaimSettings",
                            link: "/Settings/ExpenseClaimSettings",
                        }
                    ]
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Settings");
            }
        }
        if (allscreens.has("GeneralHR")) {
            if (createdMenus.has("Settings")) {
                let newObjectScreen = {
                    id: 6,
                    label: "General HR",
                    link: "/Settings/GeneralHR",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Settings");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 8,
                    label: "Settings",
                    icon: "uil-cog",
                    link: "/Settings",
                    subItems: [
                        {
                            id: 6,
                            label: "General HR",
                            link: "/Settings/GeneralHR",
                        }
                    ]
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Settings");
            }
        }
        if (allscreens.has("MyCompanies")) {
            if (createdMenus.has("Settings")) {
                let newObjectScreen = {
                    id: 7,
                    label: "My Companies",
                    link: "/Settings/MyCompanies",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Settings");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 8,
                    label: "Settings",
                    icon: "uil-cog",
                    link: "/Settings",
                    subItems: [
                        {
                            id: 7,
                            label: "My Companies",
                            link: "/Settings/MyCompanies",
                        }
                    ]
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Settings");
            }
        }
        if (allscreens.has("CurrencyTime")) {
            if (createdMenus.has("Settings")) {
                let newObjectScreen = {
                    id: 8,
                    label: "Currency & Time",
                    link: "/Settings/CurrencyTime",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Settings");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 8,
                    label: "Settings",
                    icon: "uil-cog",
                    link: "/Settings",
                    subItems: [
                        {
                            id: 8,
                            label: "Currency & Time",
                            link: "/Settings/CurrencyTime",
                        }
                    ]
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Settings");
            }
        }
        if (allscreens.has("LookFeel")) {
            if (createdMenus.has("Settings")) {
                let newObjectScreen = {
                    id: 9,
                    label: "Look & Feel",
                    link: "/Settings/LookFeel",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Settings");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 8,
                    label: "Settings",
                    icon: "uil-cog",
                    link: "/Settings",
                    subItems: [
                        {
                            id: 9,
                            label: "Look & Feel",
                            link: "/Settings/LookFeel",
                        }
                    ]
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Settings");
            }
        }
        if (allscreens.has("ExpenseClaimSettings")) {
            if (createdMenus.has("Settings")) {
                let newObjectScreen = {
                    id: 10,
                    label: "Expenses & ClaimSettings",
                    link: "/Settings/ExpenseClaimSettings",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Settings");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 8,
                    label: "Settings",
                    icon: "uil-cog",
                    link: "/Settings",
                    subItems: [
                        {
                            id: 10,
                            label: "Expense & ClaimSettings",
                            link: "/Settings/ExpenseClaimSettings",
                        }
                    ]
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Settings");
            }
        }
        if (allscreens.has("Taxes")) {
            if (createdMenus.has("Settings")) {
                let newObjectScreen = {
                    id: 11,
                    label: "Taxes",
                    link: "/Settings/Taxes",
                };
                ///add to subitem array
                index = state.userScreens
                    .map(function (e) {
                        return e.label;
                    })
                    .indexOf("Settings");
                state.userScreens[index].subItems.push(newObjectScreen);
            } else {
                let newObjectScreen = {
                    id: 8,
                    label: "Settings",
                    icon: "uil-cog",
                    link: "/Settings",
                    subItems: [
                        {
                            id: 11,
                            label: "Taxes",
                            link: "/Settings/Taxes",
                        }
                    ]
                };
                //add sub item
                state.userScreens.push(newObjectScreen);
                createdMenus.add("Settings");
            }
        }
        state.userScreens = state.userScreens.sort((a, b) =>
            a.id > b.id ? 1 : b.id > a.id ? -1 : 0,
        );
    },
    SET_TOKEN_VALUE(state, userToken) {
        state.userToken = userToken;
    },
    REMOVE_SCREENS(state) {
        state.userScreens.length = 0;
    },
};
export const actions = {
    loadUserScreens({ commit }, { userScreen }) {
        commit("LOAD_USER_SCREENS", userScreen);
    },
    loadUserToken({ commit }, { userToken }) {
        commit("SET_TOKEN_VALUE", userToken);
    },
    clearScreens({ commit }) {
        commit("REMOVE_SCREENS");
    },
};
