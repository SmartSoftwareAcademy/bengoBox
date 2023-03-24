# Generated by Django 4.1.7 on 2023-03-07 19:44

from django.db import migrations, models
import django.db.models.deletion
import django.utils.timezone


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Category',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('name', models.TextField()),
                ('description', models.TextField()),
                ('status', models.IntegerField(default=1)),
                ('date_added', models.DateTimeField(default=django.utils.timezone.now)),
                ('date_updated', models.DateTimeField(auto_now=True)),
            ],
            options={
                'verbose_name_plural': 'Categories',
                'db_table': 'categories',
                'managed': True,
            },
        ),
        migrations.CreateModel(
            name='Subcategory',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('slug', models.SlugField(max_length=255)),
                ('title', models.CharField(max_length=200)),
            ],
            options={
                'verbose_name_plural': 'Sub Categories',
                'db_table': 'subcategories',
                'managed': True,
            },
        ),
        migrations.CreateModel(
            name='Products',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('code', models.CharField(max_length=100)),
                ('name', models.TextField()),
                ('description', models.TextField()),
                ('price', models.FloatField(default=0)),
                ('status', models.IntegerField(default=1)),
                ('date_added', models.DateTimeField(default=django.utils.timezone.now)),
                ('date_updated', models.DateTimeField(auto_now=True)),
                ('category_id', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='product.category')),
            ],
            options={
                'verbose_name': 'Products',
                'verbose_name_plural': 'Products',
                'db_table': 'products',
                'managed': True,
            },
        ),
        migrations.CreateModel(
            name='ProductImages',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('image', models.FileField(upload_to='products/%Y%m%d/')),
                ('product_id', models.ForeignKey(on_delete=django.db.models.deletion.DO_NOTHING, to='product.products')),
            ],
            options={
                'verbose_name_plural': 'Product Images',
                'db_table': 'productimages',
                'managed': True,
            },
        ),
        migrations.AddField(
            model_name='category',
            name='Subcategories',
            field=models.ManyToManyField(to='product.subcategory'),
        ),
    ]
