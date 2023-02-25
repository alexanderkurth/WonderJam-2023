// Copyright Epic Games, Inc. All Rights Reserved.
/*===========================================================================
	Generated code exported from UnrealHeaderTool.
	DO NOT modify this manually! Edit the corresponding .h files instead!
===========================================================================*/

#include "UObject/GeneratedCppIncludes.h"
#include "Blingvalier/BlingvalierGameMode.h"
PRAGMA_DISABLE_DEPRECATION_WARNINGS
void EmptyLinkFunctionForGeneratedCodeBlingvalierGameMode() {}
// Cross Module References
	BLINGVALIER_API UClass* Z_Construct_UClass_ABlingvalierGameMode_NoRegister();
	BLINGVALIER_API UClass* Z_Construct_UClass_ABlingvalierGameMode();
	ENGINE_API UClass* Z_Construct_UClass_AGameModeBase();
	UPackage* Z_Construct_UPackage__Script_Blingvalier();
// End Cross Module References
	void ABlingvalierGameMode::StaticRegisterNativesABlingvalierGameMode()
	{
	}
	IMPLEMENT_CLASS_NO_AUTO_REGISTRATION(ABlingvalierGameMode);
	UClass* Z_Construct_UClass_ABlingvalierGameMode_NoRegister()
	{
		return ABlingvalierGameMode::StaticClass();
	}
	struct Z_Construct_UClass_ABlingvalierGameMode_Statics
	{
		static UObject* (*const DependentSingletons[])();
#if WITH_METADATA
		static const UECodeGen_Private::FMetaDataPairParam Class_MetaDataParams[];
#endif
		static const FCppClassTypeInfoStatic StaticCppClassTypeInfo;
		static const UECodeGen_Private::FClassParams ClassParams;
	};
	UObject* (*const Z_Construct_UClass_ABlingvalierGameMode_Statics::DependentSingletons[])() = {
		(UObject* (*)())Z_Construct_UClass_AGameModeBase,
		(UObject* (*)())Z_Construct_UPackage__Script_Blingvalier,
	};
#if WITH_METADATA
	const UECodeGen_Private::FMetaDataPairParam Z_Construct_UClass_ABlingvalierGameMode_Statics::Class_MetaDataParams[] = {
		{ "HideCategories", "Info Rendering MovementReplication Replication Actor Input Movement Collision Rendering HLOD WorldPartition DataLayers Transformation" },
		{ "IncludePath", "BlingvalierGameMode.h" },
		{ "ModuleRelativePath", "BlingvalierGameMode.h" },
		{ "ShowCategories", "Input|MouseInput Input|TouchInput" },
	};
#endif
	const FCppClassTypeInfoStatic Z_Construct_UClass_ABlingvalierGameMode_Statics::StaticCppClassTypeInfo = {
		TCppClassTypeTraits<ABlingvalierGameMode>::IsAbstract,
	};
	const UECodeGen_Private::FClassParams Z_Construct_UClass_ABlingvalierGameMode_Statics::ClassParams = {
		&ABlingvalierGameMode::StaticClass,
		"Game",
		&StaticCppClassTypeInfo,
		DependentSingletons,
		nullptr,
		nullptr,
		nullptr,
		UE_ARRAY_COUNT(DependentSingletons),
		0,
		0,
		0,
		0x008802ACu,
		METADATA_PARAMS(Z_Construct_UClass_ABlingvalierGameMode_Statics::Class_MetaDataParams, UE_ARRAY_COUNT(Z_Construct_UClass_ABlingvalierGameMode_Statics::Class_MetaDataParams))
	};
	UClass* Z_Construct_UClass_ABlingvalierGameMode()
	{
		if (!Z_Registration_Info_UClass_ABlingvalierGameMode.OuterSingleton)
		{
			UECodeGen_Private::ConstructUClass(Z_Registration_Info_UClass_ABlingvalierGameMode.OuterSingleton, Z_Construct_UClass_ABlingvalierGameMode_Statics::ClassParams);
		}
		return Z_Registration_Info_UClass_ABlingvalierGameMode.OuterSingleton;
	}
	template<> BLINGVALIER_API UClass* StaticClass<ABlingvalierGameMode>()
	{
		return ABlingvalierGameMode::StaticClass();
	}
	DEFINE_VTABLE_PTR_HELPER_CTOR(ABlingvalierGameMode);
	struct Z_CompiledInDeferFile_FID_Blingvalier_Source_Blingvalier_BlingvalierGameMode_h_Statics
	{
		static const FClassRegisterCompiledInInfo ClassInfo[];
	};
	const FClassRegisterCompiledInInfo Z_CompiledInDeferFile_FID_Blingvalier_Source_Blingvalier_BlingvalierGameMode_h_Statics::ClassInfo[] = {
		{ Z_Construct_UClass_ABlingvalierGameMode, ABlingvalierGameMode::StaticClass, TEXT("ABlingvalierGameMode"), &Z_Registration_Info_UClass_ABlingvalierGameMode, CONSTRUCT_RELOAD_VERSION_INFO(FClassReloadVersionInfo, sizeof(ABlingvalierGameMode), 2580366538U) },
	};
	static FRegisterCompiledInInfo Z_CompiledInDeferFile_FID_Blingvalier_Source_Blingvalier_BlingvalierGameMode_h_1755700065(TEXT("/Script/Blingvalier"),
		Z_CompiledInDeferFile_FID_Blingvalier_Source_Blingvalier_BlingvalierGameMode_h_Statics::ClassInfo, UE_ARRAY_COUNT(Z_CompiledInDeferFile_FID_Blingvalier_Source_Blingvalier_BlingvalierGameMode_h_Statics::ClassInfo),
		nullptr, 0,
		nullptr, 0);
PRAGMA_ENABLE_DEPRECATION_WARNINGS
