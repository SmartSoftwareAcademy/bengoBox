from django.contrib.auth.models import User
from rest_framework import routers, serializers, viewsets
from product.models import *

# Serializers define the API representation.
class ProductsSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Products
        fields =' __all__'
        
# Serializers define the API representation.
class CategoriesSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Category
        fields =' __all__'

# Serializers define the API representation.
class SalesSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Sales
        fields =' __all__'

# Serializers define the API representation.
class SalesItemsSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = salesItems
        fields =' __all__'