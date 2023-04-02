<template>
    <div class="container-fluid overflow-auto" id="receipt">
        <v-card class="receipt-card">
            <v-card-title class="receipt-title">BengoBox Commerce</v-card-title>
            <v-card-text class="receipt-text">
                <p>Receipt No: {{ receiptNo }}</p>
                <p>Date: {{ formatDate }}</p>
                <p>Time: {{ formatTime }}</p>
                <p>------------------------------------</p>
                <v-row class="m-0 p-0">
                    <v-col>#</v-col>
                    <v-col>Item</v-col>
                    <v-col>Quantity</v-col>
                    <v-col>Price</v-col>
                </v-row>
                <v-row class="receipt-items p-0 m-0" v-for="(item, index) in items" :key="index">
                    <v-col>{{ index }}</v-col>
                    <v-col>{{ item.product.name }}</v-col>
                    <v-col>{{ item.quantity }}</v-col>
                    <v-col>{{ item.price * item.quantity }}</v-col>
                </v-row>
                <p>------------------------------------</p>
                <v-row>
                    <v-col>Subtotal:</v-col>
                    <v-col>{{ formatCurrency(subtotal * total) }}</v-col>
                </v-row>
                <v-row>
                    <v-col>VAT:</v-col>
                    <v-col>{{ formatCurrency(tax * total) }}</v-col>
                </v-row>
                <v-row>
                    <v-col>Total:</v-col>
                    <v-col>{{ formatCurrency(total) }}</v-col>
                </v-row>
                <p>------------------------------------</p>
                <v-row>
                    <v-col>Served By:</v-col>
                    <v-col>{{ user }}</v-col>
                    <v-row>
                        <v-col>Payment Method:</v-col>
                        <v-col>{{ paymentMethod }}</v-col>
                    </v-row>
                </v-row>
            </v-card-text>
        </v-card>
        <v-btn class="print-btn" @click="printReceipt">Print Receipt</v-btn>
    </div>
</template>

<script>
import jsPDF from "jspdf";

export default {

    name: 'Receipt',
    props: {
        items: Array,
        total: Number,
        paymentMethod: String,
    },
    data() {
        return {
            receiptNo: 1234,
            subtotal: 0.84,
            tax: 0.16,
            user: JSON.parse(localStorage.user).username,
        };
    },
    computed: {
        formatDate() {
            const date = new Date();
            return `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
        },
        formatTime() {
            const date = new Date();
            return `${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
        }
    },
    mounted() {
    },
    methods: {
        formatCurrency(value) {
            return `Kshs.${value.toFixed(2)}`;
        },
        printReceipt() {
            const doc = new jsPDF();
            // const elementHandler = {
            //     "#receipt": function (element, renderer) {
            //         return true;
            //     }
            //};
            const source = document.querySelector("#receipt");
            doc.html(source, {
                callback: function (pdf) {
                    pdf.save("receipt.pdf");
                },
                margin: [10, 10],
                html2canvas: {
                    scale: 0.7
                },
                jsPDF: {
                    unit: "pt",
                    format: "letter",
                    orientation: "portrait"
                },
                pagebreak: {
                    avoid: ".page-break"
                },
                x: 0,
                y: 0,
                useCORS: true,
                windowWidth: 800
            });
        }
    }
};
</script>

<style>
.receipt {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.receipt-card {
    max-width: 300px;
}

.receipt-title {
    text-align: center;
}

.receipt-text {
    padding: 16px;
}

.receipt-items {
    margin-bottom: 4px;
}

.print-btn {
    margin-top: 16px;
}
</style>

