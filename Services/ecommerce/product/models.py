from datetime import datetime
from unicodedata import category
from django.db import models
from django.utils import timezone
from django.contrib.auth import get_user_model


User = get_user_model()
# Create your models here.


class Employee(models.Model):
    user = models.OneToOneField(
        User, on_delete=models.CASCADE, blank=True, null=True)
    code = models.CharField(max_length=100, blank=True)
    firstname = models.TextField()
    middlename = models.TextField(blank=True, null=True)
    lastname = models.TextField()
    gender = models.TextField(blank=True, null=True)
    dob = models.DateField(blank=True, null=True)
    contact = models.TextField()
    address = models.TextField()
    email = models.TextField()
    department_id = models.ForeignKey("Department", on_delete=models.CASCADE)
    position_id = models.ForeignKey("Position", on_delete=models.CASCADE)
    date_hired = models.DateField()
    salary = models.FloatField(default=0)
    status = models.IntegerField()
    date_added = models.DateTimeField(default=timezone.now)
    date_updated = models.DateTimeField(auto_now=True)

    def __str__(self):
        return self.firstname + ' ' + self.middlename + ' '+self.lastname + ' '

    class Meta:
        db_table = "employee"
        managed = True
        verbose_name_plural = "Employees"


class Department(models.Model):
    title = models.CharField(max_length=50)

    def __str__(self):
        return self.title

    class Meta:
        db_table = "department"
        managed = True
        verbose_name_plural = "Departments"


class Position(models.Model):
    title = models.CharField(max_length=50)

    def __str__(self):
        return self.title

    class Meta:
        db_table = "jobtitle"
        managed = True
        verbose_name_plural = "Job Titles"


class Category(models.Model):
    Subcategories = models.ManyToManyField("Subcategory")
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


class Products(models.Model):
    code = models.CharField(max_length=100)
    category_id = models.ForeignKey(Category, on_delete=models.CASCADE)
    name = models.TextField()
    description = models.TextField()
    price = models.FloatField(default=0)
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


class Sales(models.Model):
    attendant = models.ForeignKey("Employee", on_delete=models.DO_NOTHING)
    code = models.CharField(max_length=100)
    sub_total = models.FloatField(default=0)
    grand_total = models.FloatField(default=0)
    tax_amount = models.FloatField(default=0)
    tax = models.FloatField(default=0)
    tendered_amount = models.FloatField(default=0)
    amount_change = models.FloatField(default=0)
    date_added = models.DateTimeField(default=timezone.now)
    date_updated = models.DateTimeField(auto_now=True)

    def __str__(self):
        return self.code

    class Meta:
        db_table = 'sales'
        managed = True
        verbose_name = 'Sales'
        verbose_name_plural = 'Sales'


class salesItems(models.Model):
    sale_id = models.ForeignKey(Sales, on_delete=models.CASCADE)
    product_id = models.ForeignKey(Products, on_delete=models.CASCADE)
    price = models.FloatField(default=0)
    qty = models.FloatField(default=0)
    total = models.FloatField(default=0)

    class Meta:
        db_table = 'salesitems'
        managed = True
        verbose_name = 'Sales Items'
        verbose_name_plural = 'Sales Items'
