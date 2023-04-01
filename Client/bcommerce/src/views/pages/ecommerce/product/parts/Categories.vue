<template>
    <div class="col-md-12 col-sm-12 col-xs-12 overflow-auto">
        <v-card outlined>
            <v-card-title class="btn btn-light" icon>
                <v-icon class="text-dark">mdi-widgets</v-icon> Categories<router-link to="/home" class="badge badge-pill">Se
                    all></router-link>
            </v-card-title>
            <v-divider></v-divider>
            <template>
                <!-- <v-treeview :items="items" :open="[0]" :active="[5]" :selected-color="'#ffe'" activatable open-on-click
                    dense></v-treeview> -->
                <v-treeview :items="items" :open="open">
                    <template v-slot:prepend="{ item }">
                        <v-icon v-if="item.children" class="text-warning">mdi-folder</v-icon>
                        <v-icon v-else class="text-info">mdi-file</v-icon>
                    </template>
                    <template v-slot:label="{ item }">
                        <router-link v-if="item.link" :to="{ name: item.link }">{{ item.name }}</router-link>
                        <router-link :to="{ name: 'Products' }" v-else>{{ item.name }}</router-link>
                    </template>
                </v-treeview>
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
                href: '/',
            },
            {
                text: 'Clothing',
                disabled: false,
                href: '/clothing',
            },
            {
                text: 'T-Shirts',
                disabled: true,
                href: '/shirts',
            },
        ],
        items: [
            {
                id: 2,
                name: 'Supermarket',
                children: [
                    { id: 1, name: 'Point Of Sale' },
                ],
            },
            {
                id: 2,
                name: 'Health & Beauty',
                children: [
                    { id: 2, name: 'Casuals' },
                    { id: 3, name: 'Formals' },
                    { id: 4, name: 'Sneakers' },
                ],
            },
            {
                id: 2,
                name: ' Home & Office',
                children: [
                    { id: 2, name: 'Casuals' },
                    { id: 3, name: 'Formals' },
                    { id: 4, name: 'Sneakers' },
                ],
            },
            {
                id: 2,
                name: 'Appliances',
                children: [
                    { id: 2, name: 'Casuals' },
                    { id: 3, name: 'Formals' },
                    { id: 4, name: 'Sneakers' },
                ],
            },
            {
                id: 2,
                name: 'Fashion',
                children: [
                    { id: 2, name: 'Casuals' },
                    { id: 3, name: 'Formals' },
                    { id: 4, name: 'Sneakers' },
                ],
            },
            {
                id: 1,
                name: 'Gaming',
                children: [
                    { id: 5, name: 'Shirts' },
                    { id: 6, name: 'Tops' },
                    { id: 7, name: 'Tunics' },
                    { id: 8, name: 'Bodysuit' },
                ],
            },
            {
                id: 2,
                name: 'TVs & Audio',
                children: [
                    { id: 2, name: 'Phones' },
                    { id: 3, name: 'Laptops & Desktops' },
                    { id: 4, name: 'Adio Devices' },
                ],
            },
            {
                id: 10,
                name: 'Sporting Goods',
                children: [
                    { id: 5, name: 'Shirts' },
                    { id: 6, name: 'Tops' },
                    { id: 7, name: 'Tunics' },
                    { id: 8, name: 'Bodysuit' },
                ],
            },
            {
                id: 10,
                name: 'Computing',
                children: [
                    { id: 5, name: 'Shirts' },
                    { id: 6, name: 'Tops' },
                    { id: 7, name: 'Tunics' },
                    { id: 8, name: 'Bodysuit' },
                ],
            },
            {
                id: 1,
                name: 'Baby Products',
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
