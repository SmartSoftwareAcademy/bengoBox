# Generated by Django 4.1.7 on 2023-03-07 19:44

from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion
import django.utils.timezone


class Migration(migrations.Migration):

    initial = True

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
    ]

    operations = [
        migrations.CreateModel(
            name='Department',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('title', models.CharField(max_length=50)),
            ],
            options={
                'verbose_name_plural': 'Departments',
                'db_table': 'department',
                'managed': True,
            },
        ),
        migrations.CreateModel(
            name='Position',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('title', models.CharField(max_length=50)),
            ],
            options={
                'verbose_name_plural': 'Job Titles',
                'db_table': 'jobtitle',
                'managed': True,
            },
        ),
        migrations.CreateModel(
            name='Employee',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('code', models.CharField(blank=True, max_length=100)),
                ('firstname', models.TextField()),
                ('middlename', models.TextField(blank=True, null=True)),
                ('lastname', models.TextField()),
                ('gender', models.TextField(blank=True, null=True)),
                ('dob', models.DateField(blank=True, null=True)),
                ('contact', models.TextField()),
                ('address', models.TextField()),
                ('email', models.TextField()),
                ('date_hired', models.DateField()),
                ('salary', models.FloatField(default=0)),
                ('status', models.IntegerField()),
                ('date_added', models.DateTimeField(default=django.utils.timezone.now)),
                ('date_updated', models.DateTimeField(auto_now=True)),
                ('department_id', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='staff.department')),
                ('position_id', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='staff.position')),
                ('user', models.OneToOneField(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL)),
            ],
            options={
                'verbose_name_plural': 'Employees',
                'db_table': 'employee',
                'managed': True,
            },
        ),
    ]