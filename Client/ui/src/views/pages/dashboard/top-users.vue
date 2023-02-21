<script>
/**
 * Top-users component
 */
export default {
    data() {
        return {
            users: [{
                profile: require("@/assets/images/users/avatar-4.jpg"),
                name: "Glenn Holden",
                location: "Nyamasaria",
                status: "Active",
            },
            {
                profile: require("@/assets/images/users/avatar-5.jpg"),
                name: "Lolita Hamill",
                location: "Milimani",
                status: "Active",
            },
            {
                profile: require("@/assets/images/users/avatar-6.jpg"),
                name: "Robert Mercer",
                location: "Arina",
                status: "Active",
            },
            {
                profile: require("@/assets/images/users/avatar-7.jpg"),
                name: "Marie Kim",
                location: "Nyalenda",
                status: "Pending",
            },
            {
                profile: require("@/assets/images/users/avatar-8.jpg"),
                name: "Sonya Henshaw",
                location: "Wells",
                status: "Active",
            },
            ],
        };
    },
};
</script>

<template>
    <div class="col-xl-12">
        <div class="card">
            <div class="card-body">
                <div class="float-end">
                    <b-dropdown variant="white" toggle-class="p-0" menu-class="dropdown-menu-end">
                        <template v-slot:button-content>
                            <span class="text-muted">
                                Year
                                <i class="mdi mdi-chevron-down ms-1"></i>
                            </span>
                        </template>
                        <b-dropdown-item href="#">2021</b-dropdown-item>
                        <b-dropdown-item href="#">2022</b-dropdown-item>
                        <b-dropdown-item href="#">2023</b-dropdown-item>
                    </b-dropdown>
                </div>
                <h4 class="card-title mb-4">Top 5 Employees</h4>

                <div data-simplebar style="max-height: 336px;">
                    <div class="table-responsive">
                        <table class="table table-borderless table-centered table-nowrap">
                            <tbody>
                                <tr v-for="(item, index) in users" :key="index">
                                    <td style="width: 20px;">
                                        <img :src="item.profile" class="avatar-xs rounded-circle" alt="..." />
                                    </td>
                                    <td>
                                        <h6 class="font-size-15 mb-1 fw-normal">{{ item.name }}</h6>
                                        <p class="text-muted font-size-13 mb-0">
                                            <i class="mdi mdi-map-marker"></i>
                                            {{ item.location }}
                                        </p>
                                    </td>
                                    <td>
                                        <span class="badge font-size-12" :class="{
                                            'bg-soft-success': item.status === 'Success',
                                            'bg-soft-danger': item.status === 'Cancel',
                                            'bg-soft-info': item.status === 'Active',
                                            'bg-soft-warning': item.status === 'Pending'
                                        }">{{ item.status }}</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <!-- enbd table-responsive-->
                </div>
                <!-- data-sidebar-->
            </div>
            <!-- end card-body-->
        </div>
        <!-- end card-->
    </div>
    <!-- end col -->
</template>
