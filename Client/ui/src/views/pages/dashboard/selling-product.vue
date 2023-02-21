<script>
/**
 * Selling-product component
 */
export default {};
</script>

<template>
  <div class="card">
    <div class="card-body">
      <div class="float-end">
        <b-dropdown
          right
          toggle-class="text-reset p-0"
          variant="white"
          menu-class="dropdown-menu-end"
        >
          <template v-slot:button-content>
            <span class="fw-semibold">Sort By:</span>
            <span class="text-muted">
              Yearly
              <i class="mdi mdi-chevron-down ms-1"></i>
            </span>
          </template>
          <a class="dropdown-item" href="#">Monthly</a>
          <a class="dropdown-item" href="#">Yearly</a>
          <a class="dropdown-item" href="#">Weekly</a>
        </b-dropdown>
      </div>

      <h4 class="card-title mb-4">Top Rated Health Facilities</h4>

      <div class="row align-items-center no-gutters mt-3">
        <div class="col-sm-3">
          <p class="text-truncate mt-1 mb-0">
            <i class="mdi mdi-circle-medium text-primary me-2"></i> City Squre
          </p>
        </div>

        <div class="col-sm-9">
          <b-progress
            :value="52"
            variant="primary"
            class="mt-1"
            height="6px"
          ></b-progress>
        </div>
      </div>
      <!-- end row-->

      <div class="row align-items-center no-gutters mt-3">
        <div class="col-sm-3">
          <p class="text-truncate mt-1 mb-0">
            <i class="mdi mdi-circle-medium text-info me-2"></i> Agakhan
          </p>
        </div>
        <div class="col-sm-9">
          <b-progress
            :value="45"
            variant="info"
            class="mt-1"
            height="6px"
          ></b-progress>
        </div>
      </div>
      <!-- end row-->

      <div class="row align-items-center no-gutters mt-3">
        <div class="col-sm-3">
          <p class="text-truncate mt-1 mb-0">
            <i class="mdi mdi-circle-medium text-success me-2"></i> Kenyatta
          </p>
        </div>
        <div class="col-sm-9">
          <b-progress
            :value="48"
            variant="success"
            class="mt-1"
            height="6px"
          ></b-progress>
        </div>
      </div>
      <!-- end row-->

      <div class="row align-items-center no-gutters mt-3">
        <div class="col-sm-3">
          <p class="text-truncate mt-1 mb-0">
            <i class="mdi mdi-circle-medium text-warning me-2"></i>Coptic
          </p>
        </div>
        <div class="col-sm-9">
          <b-progress
            :value="78"
            variant="warning"
            class="mt-1"
            height="6px"
          ></b-progress>
        </div>
      </div>
      <!-- end row-->
    </div>
    <!-- end card-body-->
  </div>
  <!-- end card-->
</template>
