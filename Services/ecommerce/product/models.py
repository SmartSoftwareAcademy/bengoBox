from datetime import datetime
from unicodedata import category
from django.db import models
from django.utils import timezone
from django.contrib.auth import get_user_model


User = get_user_model()
# Create your models here.


class Category(models.Model):
    prioducts = models.ManyToManyField("Products")
    Subcategories = models.ManyToManyField(
        "Subcategory", null=True, blank=True)
    name = models.TextField()
    description = models.TextField()
    status = models.IntegerField(default=1)
    date_added = models.DateTimeField(default=timezone.now)
    date_updated = models.DateTimeField(auto_now=True)

    def __str__(self):
        return self.name

    class Meta:
        db_table = "categories"
        managed = True
        verbose_name_plural = "Categories"


class Subcategory(models.Model):
    slug = models.SlugField(max_length=255)
    title = models.CharField(max_length=200)

    def __str__(self):
        return self.title

    class Meta:
        db_table = "subcategories"
        managed = True
        verbose_name_plural = "Sub Categories"


class ProductImages(models.Model):
    product_id = models.ForeignKey("Products", on_delete=models.DO_NOTHING)
    image = models.FileField(upload_to="products/%Y%m%d/")

    def __str__(self):
        return self.image.url if self.image.url else None

    class Meta:
        db_table = "productimages"
        managed = True
        verbose_name_plural = "Product Images"


class Products(models.Model):
    code = models.CharField(max_length=100)
    category_id = models.ForeignKey(Category, on_delete=models.CASCADE)
    name = models.TextField()
    description = models.TextField()
    price = models.FloatField(default=0)
    discount = models.FloatField(default=0, null=True, blank=True)
    status = models.IntegerField(default=1)
    date_added = models.DateTimeField(default=timezone.now)
    date_updated = models.DateTimeField(auto_now=True)

    def __str__(self):
        return self.code + " - " + self.name

    class Meta:
        db_table = 'products'
        managed = True
        verbose_name = 'Products'
        verbose_name_plural = 'Products'
