# Generated by Django 4.1.7 on 2023-03-07 20:22

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('order', '0001_initial'),
    ]

    operations = [
        migrations.AlterModelOptions(
            name='order',
            options={'managed': True, 'ordering': ['-created_at'], 'verbose_name_plural': 'Order'},
        ),
        migrations.AlterModelOptions(
            name='orderitem',
            options={'managed': True, 'verbose_name_plural': 'Order Items'},
        ),
        migrations.AddField(
            model_name='order',
            name='confrimed',
            field=models.BooleanField(default=False),
        ),
        migrations.AddField(
            model_name='order',
            name='delivered',
            field=models.BooleanField(default=False),
        ),
        migrations.AddField(
            model_name='order',
            name='dispatched',
            field=models.BooleanField(default=False),
        ),
        migrations.AlterModelTable(
            name='order',
            table='order',
        ),
        migrations.AlterModelTable(
            name='orderitem',
            table='orderitems',
        ),
    ]