import store from "@/state/store";
const AccessScreens = store.getters["screens/getUserScreens"]

export const menuItems = AccessScreens;
console.log(AccessScreens);

// export const menuItems = [
//     {
//         id: 1,
//         label: 'menuitems.dashboard.text',
//         icon: 'uil-home-alt',
//         link: '/'
//     },
//     {
//         id: 2,
//         label: 'Pay Roll',
//         icon: 'uil-dollar-sign',
//         subItems: [{
//             id: 1,
//             label: 'Comments',
//             link: '/comments'
//         },
//         {
//             id: 2,
//             label: 'Complaints',
//             link: '/complaints'
//         },
//         {
//             id: 3,
//             label: 'Suggestions',
//             link: '/suggestions'
//         },
//         ]
//     },
//     {
//         id: 3,
//         label: 'Leave',
//         icon: 'uil-user',
//         subItems: [{
//             id: 1,
//             label: 'Comments',
//             link: '/comments'
//         },
//         {
//             id: 2,
//             label: 'Complaints',
//             link: '/complaints'
//         },
//         {
//             id: 3,
//             label: 'Suggestions',
//             link: '/suggestions'
//         },
//         ]
//     },
//     {
//         id: 4,
//         label: 'Attendance',
//         icon: 'uil-apps',
//         subItems: [{
//             id: 1,
//             label: 'Comments',
//             link: '/comments'
//         },
//         {
//             id: 2,
//             label: 'Complaints',
//             link: '/complaints'
//         },
//         {
//             id: 3,
//             label: 'Suggestions',
//             link: '/suggestions'
//         },
//         ]
//     },
//     {
//         id: 5,
//         label: 'Employees',
//         icon: 'uil-user',
//         subItems: [{
//             id: 1,
//             label: 'Comments',
//             link: '/comments'
//         },
//         {
//             id: 2,
//             label: 'Complaints',
//             link: '/complaints'
//         },
//         {
//             id: 3,
//             label: 'Suggestions',
//             link: '/suggestions'
//         },
//         ]
//     },

//     {
//         id: 4,
//         label: 'Reports',
//         icon: 'uil-file',
//         subItems: [{
//             id: 1,
//             label: 'Comments',
//             link: '/comments'
//         },
//         {
//             id: 2,
//             label: 'Complaints',
//             link: '/complaints'
//         },
//         {
//             id: 3,
//             label: 'Suggestions',
//             link: '/suggestions'
//         },
//         ]
//     },
//     {
//         id: 4,
//         label: 'Settings',
//         icon: 'uil-cog',
//         subItems: [{
//             id: 1,
//             label: 'Comments',
//             link: '/comments'
//         },
//         {
//             id: 2,
//             label: 'Complaints',
//             link: '/complaints'
//         },
//         {
//             id: 3,
//             label: 'Suggestions',
//             link: '/suggestions'
//         },
//         ]
//     },
// ]