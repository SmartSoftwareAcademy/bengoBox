from django.contrib.auth.hashers import make_password
from django.db import models
from django.core.validators import MinLengthValidator
from django.contrib.auth.models import (
    BaseUserManager, AbstractUser
)
from django.contrib.auth.models import Group

# Create your models here.

# class MyUserManager(BaseUserManager):
#     def create_user(self, username, first_name, last_name, email, phone, password=None):
#         """
#         Creates and saves a User with the given email, date of
#         birth and password.
#         """
#         if not email:
#             raise ValueError('Users must have an email address')

#         user = self.model(
#             email=self.normalize_email(email),
#             username=username,
#             first_name=first_name,
#             last_name=last_name,
#             phone=phone,
#             password=password,
#         )

#         user.set_password(password)
#         user.save(using=self._db)
#         return user

#     def create_superuser(self, username, first_name, last_name, email, phone, password=None):
#         """
#         Creates and saves a superuser with the given email, date of
#         birth and password.
#         """
#         user = self.create_user(
#             username=username,
#             first_name=first_name,
#             last_name=last_name,
#             phone=phone,
#             email=email,
#             password=password,
#         )
#         user.save(using=self._db)
#         return user


class EmailConfig(models.Model):
    from_email = models.EmailField(max_length=100)
    email_password = models.CharField(
        max_length=128, validators=[MinLengthValidator(8)])
    email_host = models.CharField(max_length=50, default="mail.tdbsoft.co.ke")
    email_port = models.CharField(max_length=5, default=465)
    use_tls = models.BooleanField(default=True)
    fail_silently = models.BooleanField(default=True)

    # def save(self, *args, **kwargs):
    #     self.email_password = make_password(self.email_password)
    #     super(EmailConfig, self).save(*args, **kwargs)

    def __str__(self) -> str:
        return self.from_email

    class Meta:
        verbose_name_plural = 'Email Configuration'


class MyUser(AbstractUser):
    email = models.EmailField(
        verbose_name='email address',
        max_length=100,
        unique=True,
    )
    phone = models.CharField(max_length=15)
    is_active = models.BooleanField(default=False)

    # objects = MyUserManager()

    USERNAME_FIELD = 'username'
    REQUIRED_FIELDS = ['phone', 'first_name', 'last_name', 'email']

    def __str__(self):
        return self.email

    # def has_perm(self, perm, obj=None):
    #     "Does the user have a specific permission?"
    #     # Simplest possible answer: Yes, always
    #     return True

    # def has_module_perms(self, app_label):
    #     "Does the user have permissions to view the app `app_label`?"
    #     # Simplest possible answer: Yes, always
    #     return True

    # @property
    # def is_staff(self):
    #     "Is the user a member of staff?"
    #     # Simplest possible answer: All admins are staff
    #     return self.is_staff
