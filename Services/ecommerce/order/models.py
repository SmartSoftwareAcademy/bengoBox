from django.db import models
from product.models import *
from staff.models import *
# Create your models here.


class Order(models.Model):
    customer = models.ForeignKey(Customer, on_delete=models.CASCADE)
    created_at = models.DateTimeField(auto_now_add=True)
    order_amount = models.DecimalField(
        max_digits=8, decimal_places=2)
    confrimed = models.BooleanField(default=False)
    dispatched = models.BooleanField(default=False)
    delivered = models.BooleanField(default=False)

    class Meta:
        ordering = ['-created_at']
        db_table = "order"
        managed = True
        verbose_name_plural = "Order"

    def __str__(self):
        return self.customer.user_id.username


class OrderItem(models.Model):
    order = models.ForeignKey(
        Order, related_name='items', on_delete=models.CASCADE)
    product = models.ForeignKey(
        Products, related_name='items', on_delete=models.CASCADE)
    price = models.DecimalField(max_digits=8, decimal_places=2)
    quantity = models.IntegerField(default=1)

    def __str__(self):
        return '%s' % self.id

    class Meta:
        db_table = "orderitems"
        managed = True
        verbose_name_plural = "Order Items"

    def get_total_price(self):
        return self.price * self.quantity
