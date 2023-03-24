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


class Address(models.Model):
    street = models.CharField(max_length=100)
    city = models.CharField(max_length=100)
    region = models.CharField(max_length=100)
    country = models.CharField(max_length=100)
    postal_code = models.CharField(max_length=10)

    def __str__(self):
        return self.street

    class Meta:
        db_table = "address"
        managed = True
        verbose_name_plural = "Address"


class Customer(models.Model):
    user_id = models.ForeignKey(User, on_delete=models.CASCADE)
    address = models.ForeignKey(Address, on_delete=models.CASCADE)

    def __str__(self):
        return self.user_id.username

    class Meta:
        db_table = "customer"
        managed = True
        verbose_name_plural = "Customers"


class Suppliers(models.Model):
    user_id = models.ForeignKey(User, on_delete=models.CASCADE)
    address = models.ForeignKey(Address, on_delete=models.CASCADE)

    def __str__(self):
        return self.user_id.username

    class Meta:
        db_table = "suppliers"
        managed = True
        verbose_name_plural = "Suppliers"
