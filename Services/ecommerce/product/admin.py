from django.contrib import admin
from .models import *

# Register your models here.
admin.site.register(Products)
admin.site.register(Category)
admin.site.register(Subcategory)
admin.site.register(ProductImages)
