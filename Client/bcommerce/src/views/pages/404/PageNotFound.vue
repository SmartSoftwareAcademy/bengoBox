<script>
import appConfig from "@/app.config";
import Layout from "../../layouts/main";
//import PageHeader from "@/components/page-header";

/**
 * 404-component
 */
export default {
    layout: "auth",
    page: {
        title: "Error-404",
        meta: [
            {
                name: "description",
                content: appConfig.description,
            },
        ],
    },
    data() {
        return {
            title: "Error-404",
        };
    },
    components: {
        Layout,
    },
    middleware: "authentication",
};
</script>

<template>
    <Layout>
        <div class="my-5 pt-sm-5">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="text-center">
                            <div>
                                <div class="row justify-content-center">
                                    <div class="col-sm-4">
                                        <div class="error-img">
                                            <img src="@/assets/images/404-error.png" alt
                                                class="img-fluid mx-auto d-block" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h4 class="text-uppercase mt-4">Sorry, page not found</h4>
                            <p class="text-muted">
                                It will be as simple as Occidental in fact, it will be Occidental
                            </p>
                            <div class="mt-5">
                                <router-link to="/" class="btn btn-primary">Back to Dashboard</router-link>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </Layout>
</template>
