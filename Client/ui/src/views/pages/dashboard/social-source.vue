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
        <div class="float-end">
          <b-dropdown
            right
            toggle-class="p-0"
            variant="white"
            menu-class="dropdown-menu-end"
          >
            <template v-slot:button-content>
              <span class="text-muted">
                Recent
                <i class="mdi mdi-chevron-down ms-1"></i>
              </span>
            </template>
            <b-dropdown-item href="#">Recent</b-dropdown-item>
            <b-dropdown-item href="#">Location</b-dropdown-item>
            <b-dropdown-item href="#">By Clients</b-dropdown-item>
          </b-dropdown>
        </div>

        <h4 class="card-title mb-4">Recent Activity</h4>

        <ol
          class="activity-feed mb-0 ps-2"
          data-simplebar
          style="max-height: 336px"
        >
          <li class="feed-item">
            <div class="feed-item-list">
              <p class="text-muted mb-1 font-size-13">
                Today
                <small class="d-inline-block ms-1">12:20 pm</small>
              </p>
              <p class="mt-0 mb-0">
                New parking fee paid - REF R7646576F:
                <span class="text-primary"> City Squre </span>
              </p>
            </div>
          </li>
          <li class="feed-item">
            <p class="text-muted mb-1 font-size-13">
              19 May, 2022
              <small class="d-inline-block ms-1">12:36 pm</small>
            </p>
            <p class="mt-0 mb-0">
              New parking lot booking request at City Squre by:
              <span class="text-primary">Designer Alex</span>
            </p>
          </li>
          <li class="feed-item">
            <p class="text-muted mb-1 font-size-13">
              19 May, 2022
              <small class="d-inline-block ms-1">07:56 am</small>
            </p>
            <p class="mt-0 mb-0">
              New client comment on parking
              <span class="text-primary">Developer Moreno</span>
            </p>
          </li>
          <li class="feed-item">
            <p class="text-muted mb-1 font-size-13">
              19 May, 2022
              <small class="d-inline-block ms-1">08:42 pm</small>
            </p>
            <p class="mt-0 mb-0">
              Pending parking fee invoice ref:
              <span class="text-primary">G655HdJU9C</span>
            </p>
          </li>

          <li class="feed-item">
            <p class="text-muted mb-1 font-size-13">
              19 May, 2022
              <small class="d-inline-block ms-1">12:22 am</small>
            </p>
            <p class="mt-0 mb-0">
              Parking reserve request approved ref:
              <span class="text-primary">G655HdJU9C</span>
            </p>
          </li>
          <li class="feed-item pb-1">
            <p class="text-muted mb-1 font-size-13">
              18 May, 2022
              <small class="d-inline-block ms-1">09:48 pm</small>
            </p>
            <p class="mt-0 mb-0">
              Parking slot update at City Squre by
              <span class="text-primary">City Admin</span>
            </p>
          </li>
        </ol>
      </div>
    </div>
  </div>
</template>
