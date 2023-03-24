from django.shortcuts import render
from datetime import date, datetime
from .models import *
from rest_framework.views import APIView
from rest_framework.response import Response

# Create your views here.


class Home(APIView):
    permission_classes = ()
    authentication_classes = ()

    def get(self, request, *args, **kwargs):
        now = datetime.now()
        current_year = now.year
        current_month = now.month
        current_day = now.day
        categories = len(Category.objects.all())
        subcategories = len(Subcategory.objects.all())
        products = len(Products.objects.all())
        transaction = len(Sales.objects.filter(
            date_added__year=current_year,
            date_added__month=current_month,
            date_added__day=current_day
        ))
        today_sales = Sales.objects.filter(
            date_added__year=current_year,
            date_added__month=current_month,
            date_added__day=current_day
        ).all()
        total_sales = sum(today_sales.values_list('grand_total', flat=True))
        context = {
            'categories': categories,
            'subcategories': subcategories,
            'products': products,
            'transaction': transaction,
            'total_sales': total_sales,
        }
        return Response(context)
