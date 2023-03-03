from django.contrib import admin
from .models  import *

# Register your models here.
admin.site.register(Products)
admin.site.register(Sales)
admin.site.register(salesItems)
admin.site.register(Category)
admin.site.register(Department)
admin.site.register(Position)
admin.site.register(Subcategory)
admin.site.register(Employee)