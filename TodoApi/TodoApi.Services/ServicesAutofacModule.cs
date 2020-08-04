using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using FluentValidation;

using MediatR;
using MediatR.Pipeline;

using Todo.Common.ServiceContracts;
using Todo.Services.PipelineBehavior;

namespace Todo.Services
{
    public class ServicesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();

            builder.RegisterTypes(GetTypesImplementingInterface(typeof(IValidator<>)).ToArray())
                .AsClosedTypesOf(typeof(IValidator<>))
                .AsImplementedInterfaces();

            builder.RegisterTypes(GetTypesImplementingInterface(typeof(IValidatedRequestHandler<,>)).ToArray())
                .AsClosedTypesOf(typeof(IValidatedRequestHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterGeneric(typeof(RequestValidationPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.RegisterType<Mediator>().As<IMediator>().SingleInstance();
        }

        private static IEnumerable<Type> GetTypesImplementingInterface(Type @interface) =>
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.IsDynamic && (x.GetName().Name?.StartsWith("Todo") ?? false))
                .SelectMany(
                    assembly => assembly.GetTypes()
                        .Where(
                            type => type.GetInterfaces()
                                        .Any(i => i == @interface || i.IsGenericType && i.GetGenericTypeDefinition() == @interface) &&
                                    !type.IsAbstract &&
                                    type.IsInNamespace("Todo")))
                .ToList();
    }
}
