from rest_framework import serializers
from .models import *
from rest_framework import status
from rest_framework.response import Response
from django.conf import settings
import re
import threading
from django.contrib.auth import get_user_model


# Serializers define the API representation.


class ProductsSerializer(serializers.ModelSerializer):
    class Meta:
        model = Products
        fields = '__all__'


class CategoriesSerializer(serializers.ModelSerializer):
    class Meta:
        model = Category
        fields = '__all__'


class SubCategoriesSerializer(serializers.ModelSerializer):
    class Meta:
        model = Subcategory
        fields = '__all__'
