<template>
    <div class="conatiner">
        <v-card>
            <v-card-title>Point Of Sale</v-card-title>
            <div class="row m-2 p-2">
                <div class="col-lg-6">
                    <div class="mb-3">
                        <v-select label="Select Product" v-model="selectedProduct" :items="products" item-value="id"
                            item-text="name" :search-input.sync="search" clearable chips></v-select>
                    </div>
                </div>
                <div class="col-lg-2">
                    <div class="mb-3">
                        <v-text-field label="Quantity" type="number" v-model="quantity"></v-text-field>
                    </div>
                </div>
            </div>
            <div class="row mb-2 p-2">
                <div class="col-lg-6">
                    <v-btn @click="addToCart" color="primary">Add to Tray</v-btn>

                </div>
            </div>
        </v-card>
        <v-card>
            <v-card-title>Tray</v-card-title>
            <v-card-text>
                <v-data-table :headers="headers" :items="cart" item-key="id">
                    <template v-slot:[`item.name`]="{ item }">{{ item.product.name }}</template>
                    <template v-slot:[`item.price`]="{ item }">{{ item.price }}</template>
                    <template v-slot:[`item.quantity`]="{ item }">{{ item.quantity }}</template>
                    <template v-slot:[`item.actions`]="{ item }">
                        <v-btn @click="removeFromCart(item.index)" icon><v-icon>mdi-delete</v-icon></v-btn>
                    </template>
                </v-data-table>
            </v-card-text>
        </v-card>
        <v-card>
            <v-card-text>
                <v-divider></v-divider>
                <div>
                    <h3>Total: KES {{ total.toFixed(2) }}</h3>
                </div>
                <div v-if="paymentMethod === 'cash'">
                    <v-text-field v-model.number="amountPaid" label="Amount Paid" class="my-2"></v-text-field>
                    <div>
                        <h2>Change: KES {{ change.toFixed(2) }}</h2>
                    </div>
                </div>
                <v-radio-group v-model="paymentMethod">
                    <v-radio value="cash" label="Cash">Cash</v-radio>
                    <v-radio value="mpesa" label="M-Pesa" v-b-modal.modal-mpesa>M-Pesa</v-radio>
                </v-radio-group>
                <v-btn color="primary" v-if="paymentMethod === 'cash'" @click="confirmPayment()">Complete
                    Transaction</v-btn>
                <v-btn @click="confirmPayment()" color="primary" v-if="paymentMethod === 'mpesa'">Confirm Payment</v-btn>
            </v-card-text>
        </v-card>
        <!--modal mpesa payment-->
        <b-modal id="modal-mpesa" ref="modal" title="Confirm Payment">
            <form ref="form" @submit.prevent="handleSubmit">
                <b-form-group label="Transaction Code" label-for="name-input"
                    invalid-feedback="Transaction Code is required">
                    <b-form-input id="name-input" v-model="mpesacode" required></b-form-input>
                </b-form-group>
            </form>
        </b-modal>
        <b-modal id="modal-receipt" ref="modal" title="Confirm Payment">
            <v-card>
                <v-card-title>Transaction Receipt</v-card-title>
                <v-card-text>
                    <div v-if="transactionCompleted">
                        <p>Transaction ID: {{ transactionId }}</p>
                        <p>Transaction Date: {{ new Date() }}</p>
                        <v-data-table :headers="receiptHeaders" :items="cart" item-key="id">
                            <template v-slot:[`item.price`]="{ item }">
                                <div>{{ item.price * item.quantity }}</div>
                            </template>
                        </v-data-table>
                        <div>
                            <p>Subtotal: {{ total.toFixed(2) / 1.16 }}</p>
                            <p>VAT (16%): {{ total.toFixed(2) / 1.16 * 0.16 }}</p>
                            <p>Total: {{ total.toFixed(2) }}</p>
                            <p>Amount Paid: {{ amountPaid }}</p>
                            <p>Change: {{ change.toFixed(2) }}</p>
                        </div>
                    </div>
                    <div v-else>Transaction not completed.</div>
                </v-card-text>
            </v-card>
        </b-modal>
    </div>
</template>
<script>
// import Multiselect from "vue-multiselect";
// import "vue-multiselect/dist/vue-multiselect.min.css";

export default {
    name: 'PosCart',
    data() {
        return {
            headers: [
                { text: 'Name', value: 'name', sortable: true },
                { text: 'Price', value: 'price', sortable: true },
                { text: 'Quantity', value: 'quantity', sortable: true },
                { text: 'Actions', value: 'actions', sortable: true }
            ],
            receiptHeaders: [
                { text: 'Name', value: 'name', sortable: true },
                { text: 'Price', value: 'price', sortable: true },
                { text: 'Quantity', value: 'quantity', sortable: true },
            ],
            products: [
                { id: 1, name: 'Product A', price: 100 },
                { id: 2, name: 'Product B', price: 200 },
                { id: 3, name: 'Product C', price: 300 },
            ],
            selectedProduct: null,
            quantity: 1,
            cart: [],
            paymentMethod: 'cash',
            amountPaid: 0,
            transactionCompleted: false,
            mpesacode: '',
            search: '',
        }
    },
    components: {
        //Multiselect,
    },
    computed: {
        total() {
            return this.cart.reduce((acc, item) => acc + item.price, 0);
        },
        change() {
            return this.amountPaid - this.total;
        },
    },
    methods: {
        addToCart() {
            const product = this.products.find((product) => product.id === this.selectedProduct);
            const quantity = this.quantity;
            const price = product.price * quantity;
            this.cart.push({ product, quantity, price });
            this.selectedProduct = null;
            this.quantity = 1;
        },
        updateCartTotal() {

        },
        updateChange() {

        },
        removeFromCart(index) {
            this.cart.splice(index, 1);
        },
        confirmPayment() {
            if (this.paymentMethod == 'cash') {
                if (Number(this.amountPaid) >= Number(this.total)) {
                    this.transactionCompleted = true;
                    this.$bvModal.show('modal-receipt')
                } else {
                    alert('Amount paid:' + this.amountPaid + ' is less than Total:' + this.total.toFixed(2))
                }
            } else if (this.paymentMethod == 'mpesa') {
                if (Number(this.amountPaid) >= Number(this.total)) {
                    this.transactionCompleted = true;
                }
                this.$bvModal.show('modal-mpesa');
            }
            this.clearValues();
        },
        handleSubmit() {

        },
        clearValues() {
            this.transactionCompleted = false;
        }
    }
}
</script>