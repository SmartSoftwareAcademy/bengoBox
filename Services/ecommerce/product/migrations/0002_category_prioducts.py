# Generated by Django 4.1.7 on 2023-03-08 04:40

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('product', '0001_initial'),
    ]

    operations = [
        migrations.AddField(
            model_name='category',
            name='prioducts',
            field=models.ManyToManyField(to='product.products'),
        ),
    ]
