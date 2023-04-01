<script>
import vue2Dropzone from "vue2-dropzone";
import Multiselect from "vue-multiselect";

import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import "vue2-dropzone/dist/vue2Dropzone.min.css";
import "vue-multiselect/dist/vue-multiselect.min.css";

/**
 * Add-product component
 */
export default {
  page: {
    title: "Add Product",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  components: {
    vueDropzone: vue2Dropzone,
    Multiselect,
    Layout,
    PageHeader,
  },
  data() {
    return {
      title: "Add Product",
      items: [
        {
          text: "Ecommerce",
        },
        {
          text: "Add Product",
          active: true,
        },
      ],
      dropzoneOptions: {
        url: "https://httpbin.org/post",
        thumbnailWidth: 150,
        maxFilesize: 0.5,
        headers: {
          "My-Awesome-Header": "header value",
        },
      },
      spec: null,
      specs: [
        "High Quality",
        "Leather",
        "Other",
      ],
      cat: null,
      cats: [
        "TV & Audio",
        "Electronics",
        "Phones & Tablets",
        "Other",
      ],
      subcat: null,
      subcats: [
        "Foot Wear",
        "Trousers",
        "Samsung",
        "iPhone",
        "Hp",
        "Dell",
        "Other",
      ],
      manufact: null,
      Manufacturers: [
        "Intel",
        "IBM",
        "Dell",
        "Other",
      ],
      brand: null,
      brands: [
        "Hp",
        "Dell",
        "Air Force",
        "Asus",
        "Other",
      ],
      images: [],
      sku: '',
      weight: '',
      dimensions: '',
      availability: 'In Stock',
      availableoptions: ['In Stock', 'Out Of Stock', 'Pre-Order'],
      color: '',
      colors: ['black', 'green', 'silver', 'gray', 'maroon'],
      size: '',
      sizes: ['XS', 'S', 'M', 'L', 'XL', 'XXL', 'XXXL']
    };
  },
  middleware: "authentication",
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div class="row">
      <div class="col-lg-12">
        <div id="addproduct-accordion" class="custom-accordion">
          <div class="card">
            <a href="javascript: void(0);" class="text-dark" data-toggle="collapse" aria-expanded="true"
              aria-controls="addproduct-billinginfo-collapse" v-b-toggle.accordion-1>
              <div class="p-4">
                <div class="media align-items-center">
                  <div class="me-3">
                    <div class="avatar-xs">
                      <div class="avatar-title rounded-circle bg-soft-primary text-primary">
                        01
                      </div>
                    </div>
                  </div>
                  <div class="media-body overflow-hidden">
                    <h5 class="font-size-16 mb-1">Product Info</h5>
                    <p class="text-muted text-truncate mb-0">
                      Fill all information below
                    </p>
                  </div>
                  <i class="mdi mdi-chevron-up accor-down-icon font-size-24"></i>
                </div>
              </div>
            </a>

            <b-collapse data-parent="#addproduct-accordion" id="accordion-1" visible accordion="my-accordion">
              <div class="p-4 border-top">
                <form @submit.prevent="handleSubmit()">
                  <div class="mb-3">
                    <label for="productname">Product Name*:</label>
                    <input id="productname" name="productname" type="text" class="form-control" v-model="product_name" />
                  </div>
                  <div class="row">
                    <div class="col-lg-3">
                      <div class="mb-3">
                        <label for="manufacturername">Manufacturer Name <button class="badge badge-pill bg-primary"
                            @click="addmanufacturere()"><i class="fa fa-plus"></i> Add</button></label>
                        <multiselect v-model="manufact" :options="Manufacturers" :multiple="false" :editable="true">
                        </multiselect>
                      </div>
                    </div>
                    <div class="col-lg-3">
                      <div class="mb-3">
                        <label class="control-label">Brand <button class="badge badge-pill bg-primary"
                            @click="addBrand()"><i class="fa fa-plus"></i> Add</button></label>
                        <multiselect v-model="brand" :options="brands" :multiple="false"></multiselect>
                      </div>
                    </div>
                    <div class="col-lg-3">
                      <div class="mb-3">
                        <label for="price">Price*:</label>
                        <input id="price" name="price" type="number" class="form-control" v-model="price" required />
                      </div>
                    </div>
                    <div class="col-lg-3">
                      <div class="mb-3">
                        <label for="discount">Discount Price</label>
                        <input id="discount" name="discount" type="number" class="form-control"
                          v-model="discount_price" />
                      </div>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col-md-6">
                      <div class="mb-3">
                        <div class="mb-3">
                          <label class="control-label">Categories <button class="badge badge-pill bg-primary"
                              @click="addCats()"><i class="fa fa-plus"></i> Add</button></label>
                          <multiselect v-model="cat" :options="cats" :multiple="false"></multiselect>
                        </div>
                      </div>
                    </div>
                    <div class="col-md-6">
                      <div class="mb-3">
                        <label class="control-label">Sub Categories <button class="badge badge-pill bg-primary"
                            @click="addSubcats()"><i class="fa fa-plus"></i> Add</button></label>
                        <multiselect v-model="subcat" :options="subcats" :multiple="true"></multiselect>
                      </div>
                    </div>
                  </div>

                  <div class="mb-3 mb-0">
                    <label for="productdesc">Product Description*:</label>
                    <textarea class="form-control" id="productdesc" rows="6" v-model="description" required></textarea>
                  </div>
                </form>
              </div>
            </b-collapse>
          </div>

          <div class="card">
            <a href="javascript: void(0);" class="text-dark collapsed" data-toggle="collapse" aria-expanded="false"
              aria-controls="addproduct-img-collapse" v-b-toggle.accordion-2>
              <div class="p-4">
                <div class="media align-items-center">
                  <div class="me-3">
                    <div class="avatar-xs">
                      <div class="avatar-title rounded-circle bg-soft-primary text-primary">
                        02
                      </div>
                    </div>
                  </div>
                  <div class="media-body overflow-hidden">
                    <h5 class="font-size-16 mb-1">Product Image</h5>
                    <p class="text-muted text-truncate mb-0">
                      Fill all information below
                    </p>
                  </div>
                  <i class="mdi mdi-chevron-up accor-down-icon font-size-24"></i>
                </div>
              </div>
            </a>

            <b-collapse id="accordion-2" accordion="my-accordion" data-parent="#addproduct-accordion">
              <div class="p-4 border-top">
                <vue-dropzone id="dropzone" ref="myVueDropzone" :use-custom-slot="true" :options="dropzoneOptions">
                  <div class="dropzone-custom-content">
                    <i class="display-4 text-muted bx bxs-cloud-upload"></i>
                    <h4>Drop files here or click to upload.</h4>
                  </div>
                </vue-dropzone>
              </div>
            </b-collapse>
          </div>

          <div class="card">
            <a href="javascript: void(0);" class="text-dark collapsed" v-b-toggle.accordion-3>
              <div class="p-4">
                <div class="media align-items-center">
                  <div class="me-3">
                    <div class="avatar-xs">
                      <div class="avatar-title rounded-circle bg-soft-primary text-primary">
                        03
                      </div>
                    </div>
                  </div>
                  <div class="media-body overflow-hidden">
                    <h5 class="font-size-16 mb-1">Meta Data</h5>
                    <p class="text-muted text-truncate mb-0">
                      Fill all that applies
                    </p>
                  </div>
                  <i class="mdi mdi-chevron-up accor-down-icon font-size-24"></i>
                </div>
              </div>
            </a>

            <b-collapse id="accordion-3" accordion="my-accordion" data-parent="#addproduct-accordion">
              <div class="p-4 border-top">
                <form>
                  <div class="row">
                    <div class="col-sm-4">
                      <div class="mb-3">
                        <label for="sku">Product SKU*:</label>
                        <input type="number" id="sku" v-model="sku" class="form-control" placeholder="Available Units"
                          required>
                      </div>
                    </div>
                    <div class="col-sm-4">
                      <div class="mb-3">
                        <label for="weight">Product Weight:</label>
                        <input type="number" id="weight" v-model="weight" min="0" step="0.01" class="form-control">
                      </div>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col-sm-4">
                      <div class="mb-3 mb-0">
                        <label for="dimensions">Product Dimensions:</label>
                        <input type="text" id="dimensions" v-model="dimensions" placeholder="LxWxH(e.g 3x2x1)"
                          class="form-control" required>
                      </div>
                    </div>
                    <div class="col-sm-4">
                      <div class="mb-3">
                        <label for="availability">Product Availability*:</label>
                        <multiselect v-model="availability" :options="availableoptions" :multiple="false"></multiselect>
                      </div>
                    </div>
                  </div>

                </form>
              </div>
            </b-collapse>
          </div>

          <div class="card">
            <a href="javascript: void(0);" class="text-dark collapsed" v-b-toggle.accordion-4>
              <div class="p-4">
                <div class="media align-items-center">
                  <div class="me-3">
                    <div class="avatar-xs">
                      <div class="avatar-title rounded-circle bg-soft-primary text-primary">
                        04
                      </div>
                    </div>
                  </div>
                  <div class="media-body overflow-hidden">
                    <h5 class="font-size-16 mb-1">Variations</h5>
                    <p class="text-muted text-truncate mb-0">
                      Fill all that applies
                    </p>
                  </div>
                  <i class="mdi mdi-chevron-up accor-down-icon font-size-24"></i>
                </div>
              </div>
            </a>

            <b-collapse id="accordion-4" accordion="my-accordion" data-parent="#addproduct-accordion">
              <div class="p-4 border-top">
                <form>
                  <div class="row">
                    <div class="col-sm-3">
                      <div class="mb-3">
                        <label for="size">Product Sizes:</label>
                        <multiselect v-model="size" :options="sizes" :multiple="true"></multiselect>
                      </div>
                    </div>
                    <div class="col-sm-3">
                      <div class="mb-3">
                        <label for="color">Product Colors:</label>
                        <multiselect v-model="color" :options="colors" :multiple="true"></multiselect>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="mb-3 mb-0">
                        <label for="dimensions">Matrial:</label>
                        <input type="text" id="material" v-model="material" placeholder="Lether" class="form-control"
                          required>
                      </div>
                    </div>
                  </div>
                </form>
              </div>
            </b-collapse>
          </div>
        </div>
      </div>
    </div>
    <div class="row mb-4">
      <div class="col text-end ms-1">
        <a href="/" class="btn btn-danger">
          <i class="uil uil-times me-1"></i> Cancel
        </a>
        <button href="#" class="btn btn-success ms-1">
          <i class="uil uil-file-alt me-1"></i> Save
        </button>
      </div>
      <!-- end col -->
    </div>
  </Layout>
</template>
