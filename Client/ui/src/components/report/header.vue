<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <form @submit.prevent="handleSubmit">
              <div class="row">
                <div class="col-sm-12 overflow-auto">
                  <h5 class="my-4">
                    Do you want to generate PDF document for {{ title }}
                  </h5>
                  <h4 class="my4 d-inline" :class="{ 'd-none': concodance == 0 }">
                    Concodance:&nbsp;<span class="text-info">{{ concodance }}&nbsp;%</span>
                  </h4>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
    <!----@click="position(), $bvModal.hide('modal-1') --->
    <div class="row">
      <div class="col-sm-12 mb-2 d-flex flex-row">
        <div class="col-sm-3 mb-2">
          <b-button variant="dark" @click="generatePDF('Excel'), $bvModal.hide('modal-1')">Print Excel
          </b-button>
        </div>
        <div class="col-sm-3 mb-2">
          <b-button variant="dark" @click="generatePDF('pdf'), $bvModal.hide('modal-1')">Print PDF
          </b-button>
        </div>
      </div>
      <div class="col-sm-6 mb-2">
        <p class="text-dark fw-bold text-decoration-underline">
          PDF page options
        </p>
        <div class="d-flex flex-row">
          <b-form-checkbox id="checkbox-1" v-model="pagelayout" name="checkbox-1" value="l" unchecked-value="p">&nbsp;
            Landscape </b-form-checkbox>&nbsp;
          <b-form-checkbox id="checkbox-2" v-model="pagelayout" name="checkbox-2" value="p" unchecked-value="p">&nbsp;
            Potrait
          </b-form-checkbox>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import jsPDF from "jspdf";
import autoTable from "jspdf-autotable";
//import axios from "axios";

//import Tahoma from "@/assets/fonts/tahoma.ttf";
import Swal from "sweetalert2";

export default {
  name: "report",
  props: {
    title: String,
    reportfor: String,
    rpt: String,
    records: Array,
    pl: String,
    headers: Object,
    uniqueCars: Object,
    showme: Boolean,
    printedpdf: Boolean,
    concodance: Number,
  },
  data() {
    return {
      pagelayout: "l",
    };
  },
  methods: {
    makepdf() {
      //    alert("");
      //  window.html2canvas = html2canvas;
      var doc = new jsPDF("l", "pt", "a4");
      doc.html(document.querySelector("#print"), {
        callback: function (pdf) {
          pdf.save("make.pdf");
        },
      });
    },
    gen() {
      autoTable();
    },
    generatePDF(event) {
      //alert();
      const d = new Date();
      const year = d.getFullYear();
      const month = d.getMonth();
      const date = d.getDate();

      const hour = d.getHours();
      const min = d.getMinutes();
      const sec = d.getSeconds();
      const msec = d.getMilliseconds();

      const filename =
        year +
        "-" +
        month +
        "-" +
        date +
        "-" +
        hour +
        "-" +
        min +
        "-" +
        sec +
        "-" +
        msec;

      var doc = new jsPDF(this.pagelayout);

      doc.setFillColor(255, 255, 255);
      doc.roundedRect(5, 5, 200, 30, 1, 1, "FD");
      doc.setTextColor(0, 0, 0);

      doc.setFontSize(10);
      doc.addFont("Tahoma", "Tahoma", "bold");
      doc.setFont("Tahoma");
      doc.text(60, 10, "BengoBox HR");
      doc.text(60, 15, "TDBSoft, Sustaining Information Systems");
      doc.text(60, 20, "P.O. BOX 1502 - 40100 KISUMU, KENYA");
      doc.text(60, 25, "Phone: 0743 793 901 | Email: info@tdbsoft.co.ke");
      doc.text(150, 30, "Print Date: " + this.getcurrentdate(new Date()));
      //doc.text(10, 40, this.title);
      doc.text(60, 33, "Organisation Name: BengoHub");
      doc.setFontSize(14);
      doc.addFont("Tahoma", "Tahoma", "bold");

      const logo = require("@/assets/images/logo.png");
      const klogo = require("@/assets/logo.png");
      var imgLogo = new Image();
      var imgKLogo = new Image();
      imgLogo.src = logo;
      imgKLogo.src = klogo;
      doc.addImage(imgLogo, "PNG", 10, 5, 40, 30, 5, 5);
      doc.setFillColor(255, 255, 254);
      doc.roundedRect(1, 1, 14, 9, 1, 1, "F");
      doc.addImage(imgKLogo, "PNG", 1, 1, 12, 7, 3, 3);

      doc.setFillColor(0, 255, 0);
      doc.addFont("Tahoma", "Tahoma", "bold");
      doc.setFontSize(9);
      try {
        doc.text(10, 44, this.rpt);
      } catch (e) {
        console.log(e);
      }

      // Simple data example
      var head = [this.headers];
      var body = this.uniqueCars;

      if (event == "Excel") {
        //alert(this.records.length);
        if (this.records.length == 0) {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "No Data ",
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });
        }
        const csvRows = [];
        const headers = Object.keys(this.records[0]);

        csvRows.push(headers.join(","));
        //loop over the headers
        for (const row of this.records) {
          const values = headers.map((header) => {
            const escaped = ("" + row[header]).replace(/"/g, '\\"');
            return '"' + escaped + '"';
          });
          csvRows.push(values.join(","));
        }
        //alert(csvData);
        const csvData = csvRows.join("\n");
        //alert(csvData);

        const blob = new Blob([csvData], { type: "textcsv" });
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.setAttribute("hidden", "");
        a.setAttribute("href", url);
        a.setAttribute("download", this.title + filename + ".csv");
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
      } else {
        //alert();
        doc.autoTable({
          head: head,
          body: body,
          // margin: { left: 5.5, top: 52 },
          startY: 44,
          margin: { horizontal: 0 },
          styles: {
            columnWidth: "auto",
            fontSize: 7,
            overflow: "linebreak",
            cellWidth: "auto",
          },
          columnStyles: {
            2: { cellWidth: "auto" },
            nil: { halign: "left" },
            tgl: { halign: "left" },
          },
          headerStyles: {
            halign: "center",
            fillColor: [0, 150, 120],
            textColor: [255, 255, 255],
            lineColor: [0, 0, 0],
          },
          bodyStyles: { lineColor: [0, 0, 0] },
          theme: "grid",
        });
        // Simple html example
        //alert();
        // doc.autoTable({ html: "#table" });
        //alert(filename);
        //page numbering
        var height = 190;
        var width = 290;
        if (this.pagelayout == "l") {
          height = 190;
          width = 200;
        }
        const pageCount = doc.internal.getNumberOfPages();
        for (var i = 1; i <= pageCount; i++) {
          doc.setPage(i);
          doc.text(
            "TDBSoft | BengoHub | BetahoodAfrica |                                     Prepared by:titusowuor30@gmail.com" +
            "                                 Page " +
            String(i) +
            " of " +
            String(pageCount),
            height,
            width,
            null,
            null,
            "right"
          );
        }
        //this.showme = false;
        doc.save(this.title + "" + filename + ".pdf");
        doc.output("dataurlnewwindow");
        //this.$emit("close");
        this.$bvModal.hide("modal-Print");
      }
    },

    getmonth(d) {
      const monthNames = [
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec",
      ];
      return monthNames[d];
    },
    getcurrentdate(mydate) {
      let d = new Date(mydate);
      let year = d.getFullYear();
      let month = this.getmonth(d.getMonth());
      let date = d.getDate();
      date = this.getv(date);
      //month = this.getv(month);

      let hour = d.getHours();
      let min = d.getMinutes();
      let sec = d.getSeconds();
      hour = this.getv(hour);
      min = this.getv(min);
      sec = this.getv(sec);

      //const msec = d.getMilliseconds();
      const datetime =
        date + "/" + month + "/" + year + " " + hour + ":" + min + ":" + sec;
      return datetime;
    },
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
    },
  },
};
</script>

<style>

</style>
