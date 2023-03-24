from django.db import models
from product.models import Products

# Create your models here.


class Cart(models.Model):
    product_id = models.ForeignKey(
        Products, on_delete=models.SET_NULL, related_name='products', null=True)
    quantity = models.PositiveIntegerField(default=0)

    def __str__(self) -> str:
        return "{} [{}]".format(self.product_id.name, self.quantity)

    class Meta:
        db_table = 'cart'
        managed = True
        verbose_name = 'Cart'
        verbose_name_plural = 'cart'
