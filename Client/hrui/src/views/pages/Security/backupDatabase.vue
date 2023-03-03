<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import schedulebackup from "@/components/widgets/security/schedulebackup";

export default {
  page: {
    title: "Schedule Backup",
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
    schedulebackup,
  },
  data() {
    return {
      title: "Schedule Backup ",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Schedule Backup",
          active: true,
        },
      ],
      email: "",
      role: [{ name: "Administrator" }, { name: "Prosecuter" }],
      userName: "",
      Organization: "KeNHA",
      password: "",
      cpassword: "",
      send: [],
    };
  },
  mounted() {
    //
  },
  method: {
    handleSubmit() {
      console.log("Error on submit");
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <schedulebackup :title="title" :items="items" :ordeData="orderData">
    </schedulebackup>
  </Layout>
</template>
