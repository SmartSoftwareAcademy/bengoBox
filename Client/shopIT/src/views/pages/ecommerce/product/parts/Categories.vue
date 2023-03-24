<template>
    <div class="col-md-12 col-sm-12 col-xs-12 overflow-auto">
        <v-card outlined>
            <v-card-title>Categories <router-link to="/home" class="badge badge-pill">Se
                    all></router-link></v-card-title>
            <v-divider></v-divider>
            <template>
                <v-treeview :items="items" :open="[1]" :active="[5]" :selected-color="'#ffe'" activatable open-on-click
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
        breadcrums: [
            {
                text: 'Home',
                disabled: false,
                href: 'breadcrumbs_home',
            },
            {
                text: 'Clothing',
                disabled: false,
                href: 'breadcrumbs_clothing',
            },
            {
                text: 'T-Shirts',
                disabled: true,
                href: 'breadcrumbs_shirts',
            },
        ],
        items: [
            {
                id: 2,
                name: 'Shoes',
                children: [
                    { id: 2, name: 'Casuals' },
                    { id: 3, name: 'Formals' },
                    { id: 4, name: 'Sneakers' },
                ],
            },
            {
                id: 1,
                name: 'Clothing',
                children: [
                    { id: 5, name: 'Shirts' },
                    { id: 6, name: 'Tops' },
                    { id: 7, name: 'Tunics' },
                    { id: 8, name: 'Bodysuit' },
                ],
            }
        ],
    }),
}
</script>
