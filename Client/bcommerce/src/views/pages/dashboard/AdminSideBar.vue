<template>
    <div class="col-md-12 col-sm-12 col-xs-12 overflow-auto">
        <v-card outlined>
            <v-card-title class="btn btn-light" icon>
                <v-icon class="text-dark">mdi-widgets</v-icon> Categories<router-link to="/home" class="badge badge-pill">Se
                    all></router-link>
            </v-card-title>
            <v-divider></v-divider>
            <template>
                <v-treeview :items="items" :open="[0]" :active="[5]" :selected-color="'#ffe'" activatable open-on-click
                    dense></v-treeview>
            </template>
            <v-divider></v-divider>
        </v-card>
    </div>
</template>
<script>
export default {
    data: () => ({
        page: 1,
        items: [
            {
                id: 1,
                name: 'Point of Sale',
                children: [
                    { id: 1, name: 'POS', link: 'Pos' },
                ],
            },
            {
                id: 2,
                name: 'Products',
                children: [
                    { id: 2, name: 'Add product', link: 'Addproduct' },
                    { id: 3, name: 'Products', link: 'Products' },
                    { id: 4, name: 'Add Categories', link: 'AddCategories' },
                    { id: 5, name: 'Categories', link: 'Categories' },
                    { id: 6, name: 'Add Sub-Categories', link: 'AddSubCategories' },
                    { id: 7, name: 'Sub-Categories', link: 'SubCategories' },

                ],
            },
            {
                id: 3,
                name: 'Invoices',
                children: [
                    { id: 1, name: 'Add Invoice', link: 'AddInvoice' },
                    { id: 2, name: 'Invoices', link: 'Invoices' },
                ],
            },
            {
                id: 4,
                name: 'Orders',
                children: [
                    { id: 1, name: 'Orders', link: 'Orders' },
                ],
            },
            {
                id: 5,
                name: 'Sales',
                children: [
                    { id: 1, name: 'Sales', link: 'Sales' },
                    { id: 2, name: 'Returns', link: 'Returns' },
                ],
            },

        ],
    }),
}
</script>
