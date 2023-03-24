<template>
    <div class="container-fluid bg-warning p-4">
        <PageHeader />
        <div class="card">
            <div class="card-body">
                <div class="card-title">
                    <router-link :to="{ name: 'home' }" class="btn btn-light bg-warning text-white"><i
                            class="fa fa-arrow-left"></i>
                        Continue
                        Shopping</router-link>
                </div>
                <h1>404 Page Not Found</h1>
                <p>The page you requested does not exist.</p>
            </div>
        </div>
    </div>
</template>

<script>
import PageHeader from "@/components/page-header";

export default {
    name: 'PageNotFound',
    data() {
        return {

        }
    },
    components: {
        PageHeader,
    }
}
</script>
