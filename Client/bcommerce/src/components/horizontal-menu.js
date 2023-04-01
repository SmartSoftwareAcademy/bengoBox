export const menuItems = [
    {
        id: 1,
        label: "SITE",
        isTitle: true
    },
    {
        id: 2,
        label: "Home",
        icon: "uil-home-alt",
        badge: {
            variant: "primary",
            text: "GO"
        },
        link: "/"
    },
    {
        id: 3,
        label: "menuitems.apps.text",
        isTitle: true
    },
    {
        id: 4,
        label: "Point Of Sale",
        icon: "uil-dollar-sign",
        link: "/calendar"
    },
    {
        id: 5,
        label: "menuitems.chat.text",
        icon: "uil-comments-alt",
        link: "/chat",
        badge: {
            variant: "warning",
            text: "menuitems.chat.badge"
        }
    },
    {
        id: 6,
        label: "Manage Products",
        icon: "uil-store",
        subItems: [
            {
                id: 6,
                label: "menuitems.ecommerce.list.products",
                link: "/ecommerce/products",
                parentId: 6
            },
            {
                id: 7,
                label: "menuitems.ecommerce.list.addproduct",
                link: "/ecommerce/add-product",
                parentId: 6
            },
            {
                id: 8,
                label: "Categories",
                link: "/ecommerce/categories",
                parentId: 6
            },
            {
                id: 9,
                label: "Sub Categories",
                link: "/ecommerce/subcategories",
                parentId: 6
            },
            {
                id: 11,
                label: "Manage Cart",
                link: "/ecommerce/cart",
                parentId: 6
            },
            {
                id: 13,
                label: "Manage Branches",
                link: "/ecommerce/shops",
                parentId: 6
            },
        ]
    },
    {
        id: 15,
        label: "Inbox",
        icon: "uil-envelope",
        subItems: [
            {
                id: 16,
                label: "menuitems.email.list.inbox",
                link: "/email/inbox",
                parentId: 15
            }
        ]
    },
    {
        id: 18,
        label: "Documents",
        icon: "uil-invoice",
        subItems: [
            {
                id: 19,
                label: "Invoices",
                link: "/invoices/list",
                parentId: 18
            },
            {
                id: 9,
                label: "menuitems.ecommerce.list.orders",
                link: "/ecommerce/orders",
                parentId: 6
            },
        ]
    },
    {
        id: 18,
        label: "Customers",
        icon: "uil-invoice",
        subItems: [
            {
                id: 10,
                label: "menuitems.ecommerce.list.customers",
                link: "/ecommerce/customers",
                parentId: 6
            },
            {
                id: 10,
                label: "Add Customer",
                link: "/ecommerce/customers",
                parentId: 6
            },
        ]
    },
    {
        id: 21,
        label: "Security",
        icon: "uil-book-alt",
        subItems: [
            {
                id: 23,
                label: "Users",
                link: "/contacts/list",
                parentId: 21
            }
        ]
    },
    {
        id: 25,
        label: "EXTRA",
        isTitle: true
    },
    {
        id: 31,
        label: "More",
        icon: "uil-apps",
        subItems: [
            {
                id: 35,
                label: "menuitems.utility.list.comingsoon",
                link: "/utility/comingsoon",
                parentId: 31
            },
            {
                id: 36,
                label: "menuitems.utility.list.timeline",
                link: "/utility/timeline",
                parentId: 31
            },
            {
                id: 37,
                label: "menuitems.utility.list.faqs",
                link: "/utility/faqs",
                parentId: 31
            },
            {
                id: 38,
                label: "menuitems.utility.list.pricing",
                link: "/utility/pricing",
                parentId: 31
            }
        ]
    },
];

