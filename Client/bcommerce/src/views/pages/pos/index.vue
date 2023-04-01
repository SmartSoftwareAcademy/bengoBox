<template>
    <div>
        <Layout>
            <PageHeader :title="title" :items="items" />
            <poscart />
        </Layout>
    </div>
</template>
<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import poscart from './poscart.vue';

export default {
    data() {
        return {
            title: "POS",
            items: [
                {
                    text: "POS",
                },
                {
                    text: "POS",
                    active: true,
                },
            ],
        }
    },
    components: {
        poscart,
        Layout,
        PageHeader,
    },
    methods: {
        visitRoute() {
            this.$router.push({ name: 'Post' })
        }
    },
}
</script>