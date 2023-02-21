<script>
/**
 * Activity component
 */
export default {};
</script>

<template>
  <div class="col-xl-4">
    <div class="card">
      <div class="card-body">
        <!-- <div class="float-end">
          <b-dropdown right toggle-class="p-0" variant="white" menu-class="dropdown-menu-end">
            <template v-slot:button-content>
             <span class="text-muted">
                Yesterday
                <i class="mdi mdi-chevron-down ms-1"></i>
              </span>
            </template>
            <b-dropdown-item href="#">Yesterday</b-dropdown-item>
            <b-dropdown-item href="#">Today</b-dropdown-item>
            <b-dropdown-item href="#">Last Week</b-dropdown-item>
          </b-dropdown>
        </div> -->

        <h4 class="card-title mb-4">PAYROLL HISTORY</h4>

        <ol class="activity-feed mb-0 ps-2" data-simplebar style="max-height: 336px">
          <li class="feed-item">
            <div class="feed-item-list">
              <p class="mt-0 mb-0">
                No history yet!
              </p>
            </div>
          </li>
        </ol>
      </div>
    </div>
  </div>
</template>
